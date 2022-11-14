using System.Text.Json;
using Redux;
using Redux.Basic;
using Message;
using Counter;

namespace Composite;

public class GlobalTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test()
    {
        var state = CompositeState.initState();
        var reducers = buildReducer();

        var enhancers = Redux.Middleware.applyMiddleware<CompositeState>(
            Redux.Middlewares.logMiddleware<CompositeState>((state) => JsonSerializer.Serialize(state), "CompositeTests"),
            Redux.Middlewares.exceptionMiddleware<CompositeState>("CompositeTests"),
            Redux.Middlewares.performanceMiddleware<CompositeState>("CompositeTests")
        );

        Store<CompositeState> store = Creator.createStore<CompositeState>(state, reducers, enhancers);

        store.Subscribe(() =>
        {
            CompositeState lastState = store.GetState();
            Console.WriteLine($"[Subscribe] last-state:{JsonSerializer.Serialize(lastState)}");
        });

        store.Dispatch(CounterActionCreator.add(1));
        store.Dispatch(CounterActionCreator.minus(2));
        store.Dispatch(MessageActionCreator.modify(new { Id = 1, Content = "helloword" }));

        Assert.IsTrue(state.Counter.Count == 0);
        Assert.IsTrue(state.Message.Id == 0 && state.Message.Content == "test");

        Assert.IsTrue(store.GetState().Counter.Count == -1);
        Assert.IsTrue(store.GetState().Message.Id == 1 && store.GetState().Message.Content == "helloworld");
    }

    Reducer<CompositeState> buildReducer()
    {
        ////var map = new Dictionary<string, dynamic>();
        ////map.Add("Counter", CounterReducer.buildReducer());
        ////map.Add("Message", MessageReducer.buildReducer());
        ////return Redux.Framework.ReduxHelper.combineReducers<GlobalState>(map);
        return null;
    }
}