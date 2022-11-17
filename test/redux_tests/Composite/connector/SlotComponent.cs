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
#pragma warning disable CS8625 // 无法将 null 字面量转换为非 null 的引用类型。
     reducer: Message.MessageReducer.buildReducer(), null
#pragma warning restore CS8625 // 无法将 null 字面量转换为非 null 的引用类型。
   )
    { }
}
