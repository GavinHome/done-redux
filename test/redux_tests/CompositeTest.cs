using Counter;
using Message;
using Redux;
using Redux.Basic;
using Redux.Dependencies.Basic;
using System.Text.Json;

namespace Composite;

public class CompositeTests
{

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test()
    {
        var state = CompositeState.initState();
        var reducers = CompositeReducer.buildReducer();

        var enhancers = Redux.Middleware.applyMiddleware<CompositeState>(
            Redux.Middlewares.logMiddleware<CompositeState>((state) => JsonSerializer.Serialize(state), "CompositeTests"),
            Redux.Middlewares.exceptionMiddleware<CompositeState>("CompositeTests"),
            Redux.Middlewares.performanceMiddleware<CompositeState>("CompositeTests")
        );

        Dependencies<CompositeState> dependencies = new Dependencies<CompositeState>(
            slots: new Dictionary<string, Dependent<CompositeState>>()
            {
                { "counter",  new CounterConnector().add(new CounterComponent())  },
                { "message",  new MessageConnector().add(new MessageComponent())  }
            }
        );

        Store<CompositeState> store = Creator.createStore<CompositeState>(state, reducers, enhancers, dependencies);

        store.Subscribe(() =>
        {
            CompositeState lastState = store.GetState();
            Console.WriteLine($"[Subscribe] last-state:{JsonSerializer.Serialize(lastState)}");
        });

        store.Dispatch(CounterActionCreator.add(1));

        Assert.IsTrue(store.GetState().Counter.Count == 1);
        Assert.IsTrue(store.GetState().Message.Id == 0 && store.GetState().Message.Content == "test");

        store.Dispatch(CounterActionCreator.minus(2));

        Assert.IsTrue(store.GetState().Counter.Count == -1);
        Assert.IsTrue(store.GetState().Message.Id == 0 && store.GetState().Message.Content == "test");

        store.Dispatch(MessageActionCreator.modify(1, "helloworld"));

        Assert.IsTrue(store.GetState().Counter.Count == -1);
        Assert.IsTrue(store.GetState().Message.Id == 1 && store.GetState().Message.Content == "helloworld");

        Assert.IsTrue(state.Counter.Count == 0);
        Assert.IsTrue(state.Message.Id == 0 && state.Message.Content == "test");
    }
}