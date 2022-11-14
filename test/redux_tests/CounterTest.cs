using Redux.Basic;
using Redux.Framework;
using System.Text.Json;
using Action = Redux.Basic.Action;

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

        var enhancers = Redux.Middleware.applyMiddleware<CounterState>(
            Redux.Middlewares.logMiddleware<CounterState>((state) => JsonSerializer.Serialize(state), "CounterTests"),
            Redux.Middlewares.exceptionMiddleware<CounterState>("CounterTests"),
            Redux.Middlewares.performanceMiddleware<CounterState>("CounterTests")
        );

        Store<CounterState> store = Redux.Creator.createStore<CounterState>(state, CounterReducer.buildReducer(), enhancers);

        store.Subscribe(() =>
        {
            CounterState lastState = store.GetState();
            Console.WriteLine($"[Subscribe] last-state:{JsonSerializer.Serialize(lastState)}");
        });

        store.Dispatch(CounterActionCreator.add(1));
        store.Dispatch(CounterActionCreator.minus(2));

        ////await store.dispatch(CounterActionCreator.onCompute);

        Assert.IsTrue(state.Count == 0);
        Assert.IsTrue(store.GetState().Count == -1);
    }

    [Test]
    public void TestSubscribeCancel()
    {
        var state = CounterState.initState();

        var enhancers = Redux.Middleware.applyMiddleware<CounterState>(
            Redux.Middlewares.logMiddleware<CounterState>((state) => JsonSerializer.Serialize(state), "CounterTests"),
            Redux.Middlewares.exceptionMiddleware<CounterState>("CounterTests"),
            Redux.Middlewares.performanceMiddleware<CounterState>("CounterTests")
        );

        Store<CounterState> store = Redux.Creator.createStore<CounterState>(state, CounterReducer.buildReducer(), enhancers);

        Unsubscribe subscribe = store.Subscribe(() =>
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
    public void TestBindCreators()
    {
        var state = CounterState.initState();

        var enhancers = Redux.Middleware.applyMiddleware<CounterState>(
            Redux.Middlewares.logMiddleware<CounterState>((state) => JsonSerializer.Serialize(state), "CounterTests"),
            Redux.Middlewares.exceptionMiddleware<CounterState>("CounterTests"),
            Redux.Middlewares.performanceMiddleware<CounterState>("CounterTests")
        );

        Store<CounterState> store = Redux.Creator.createStore<CounterState>(state, CounterReducer.buildReducer(), enhancers);

        store.Subscribe(() =>
        {
            CounterState lastState = store.GetState();
            Console.WriteLine($"[Subscribe] last-state:{JsonSerializer.Serialize(lastState)}");
        });


        //store.Dispatch(CounterActionCreator.add(1));
        var add = bindCreator.bindActionCreator<int>(CounterActionCreator.add, store.Dispatch);
        add(1);

        //store.Dispatch(CounterActionCreator.minus(2));
        CounterActionCreator.minus(2).Dispatch(store.Dispatch);

        ////TODO: invoke class member
        ////CounterActionCreators obj = bindCreator.bindActionCreators<CounterActionCreators>(new CounterActionCreators(), store.Dispatch);
        ////obj.add(1);

        Assert.IsTrue(state.Count == 0);
        Assert.IsTrue(store.GetState().Count == -1);
    }
}