using System.Text.Json;
using Redux.Basic;
using Message;

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

        var enhancers = Redux.Middleware.applyMiddleware<MessageState>(
            Redux.Middlewares.logMiddleware<MessageState>((state) => JsonSerializer.Serialize(state), "MessageTests"),
            Redux.Middlewares.exceptionMiddleware<MessageState>("MessageTests"),
            Redux.Middlewares.performanceMiddleware<MessageState>("MessageTests")
        );

        Store<MessageState> store = Redux.Creator.createStore<MessageState>(state, MessageReducer.buildReducer(), enhancers);

        store.Subscribe(() =>
        {
            MessageState lastState = store.GetState();
            Console.WriteLine($"[Subscribe] last-state:{JsonSerializer.Serialize(state)}");
        });

        store.Dispatch(MessageActionCreator.modify(new { Id = 1, Content = "helloword" }));

        Assert.IsTrue(state.Id == 0);
        Assert.IsTrue(state.Content == "test");
    }
}