namespace Composite;

internal class CounterComponent : Redux.Component<Counter.CounterState>
{
    public CounterComponent() : base(
         //view: buildView,
         //effect: buildEffect(),
         reducer: Counter.CounterReducer.buildReducer()
       )
    { }
}

internal class MessageComponent : Redux.Component<Message.MessageState>
{
    public MessageComponent() : base(
     //view: buildView,
     //effect: buildEffect(),
     reducer: Message.MessageReducer.buildReducer(), null
   )
    { }
}
