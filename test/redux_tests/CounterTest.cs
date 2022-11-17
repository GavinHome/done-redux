using Redux;

namespace Counter;

public class CounterTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test()
    {
        var state = CounterState.initState();
        var reducer = CounterReducer.buildReducer();
        var store = StoreCreator.createStore<CounterState>(state, reducer);

        store.Subscribe(() =>
        {
            var lastState = store.GetState();
            var stateJson = System.Text.Json.JsonSerializer.Serialize(lastState);
            Console.WriteLine($"[Subscribe] last-state:{stateJson}");
        });

        store.Dispatch(CounterActionCreator.add(1));
        store.Dispatch(CounterActionCreator.minus(2));

        Assert.IsTrue(state.Count == 0);
        Assert.IsTrue(store.GetState().Count == -1);
    }

    [Test]
    public void TestSubscribeCancel()
    {
        var state = CounterState.initState();

        var enhancers = Enhancers.applyMiddleware<CounterState>(
            Middlewares.logMiddleware<CounterState>((state) =>
                System.Text.Json.JsonSerializer.Serialize(state), "CounterTests"),
            Middlewares.exceptionMiddleware<CounterState>("CounterTests"),
            Middlewares.performanceMiddleware<CounterState>("CounterTests")
        );

        var store = StoreCreator.createStore<CounterState>(state, CounterReducer.buildReducer(), enhancers);

        var subscribe = store.Subscribe(() =>
        {
            Assert.IsTrue(1 != 1);
        });

        ////unsubscribe
        var result = subscribe.Cancel();

        store.Dispatch(CounterActionCreator.add(1));
        store.Dispatch(CounterActionCreator.minus(2));

        Assert.IsTrue(result);
        Assert.IsTrue(state.Count == 0);
        Assert.IsTrue(store.GetState().Count == -1);
    }

    [Test]
    public void TestBindCreator()
    {
        Aop.setTest();

        var state = CounterState.initState();

        var enhancers = Enhancers.applyMiddleware<CounterState>(
            Middlewares.logMiddleware<CounterState>((state) =>
                System.Text.Json.JsonSerializer.Serialize(state), "CounterTests"),
            Middlewares.exceptionMiddleware<CounterState>("CounterTests"),
            Middlewares.performanceMiddleware<CounterState>("CounterTests")
        );

        var store = StoreCreator.createStore<CounterState>(state, CounterReducer.buildReducer(), enhancers);

        store.Subscribe(() =>
        {
            CounterState lastState = store.GetState();
            Console.WriteLine($"[Subscribe] last-state:{System.Text.Json.JsonSerializer.Serialize(lastState)}");
        });

        ////store.Dispatch(CounterActionCreator.add(1));
        var add = ActionCreator.bind<int>(CounterActionCreator.add, store.Dispatch);
        add(1);

        var addAction = ActionCreator.bindAction<int>(CounterActionCreator.add, store.Dispatch);
        var action = addAction(1);
        Assert.IsTrue(action.Type.Equals(CounterAction.add));
        Assert.IsTrue(action.Payload == 1);

        //store.Dispatch(CounterActionCreator.minus(2));
        //CounterActionCreator.minus(2).Dispatch(store.Dispatch);
        var minus = ActionCreator.bindAction(CounterActionCreator.minus(3), store.Dispatch);
        minus();

        Assert.IsTrue(state.Count == 0);
        Assert.IsTrue(store.GetState().Count == -1);
    }
}