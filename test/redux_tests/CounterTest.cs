using System.Text.Json;
using Redux.Basic;
using Counter;

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
}