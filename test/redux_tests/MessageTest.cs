using System.Text.Json;

namespace Message;

public class MessageTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test()
    {
        var state = MessageState.initState();

        var enhancers = Redux.Enhancers.applyMiddleware<MessageState>(
            Redux.Middlewares.logMiddleware<MessageState>((state) => JsonSerializer.Serialize(state), "MessageTests"),
            Redux.Middlewares.exceptionMiddleware<MessageState>("MessageTests"),
            Redux.Middlewares.performanceMiddleware<MessageState>("MessageTests")
        );

        var store = Redux.StoreCreator.createStore<MessageState>(state, MessageReducer.buildReducer(), enhancers);

        store.Subscribe(() =>
        {
            MessageState lastState = store.GetState();
            Console.WriteLine($"[Subscribe] last-state:{JsonSerializer.Serialize(state)}");
        });

        //store.Dispatch(MessageActionCreator.modify(1, "helloworld"));
        var modify = Redux.ActionCreator.bind<int, string>(MessageActionCreator.modify, store.Dispatch);
        modify(1, "helloworld");

        Assert.IsTrue(state.Id == 0);
        Assert.IsTrue(state.Content == "test");
    }
}