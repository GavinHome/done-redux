using Counter;
using Message;
using Redux.Component;

namespace Composite;

internal class CounterComponent : Component<CounterState> //: AbstractLogic<CounterState>
{
    public CounterComponent() : base(
         //view: buildView,
         //effect: buildEffect(),
         reducer: CounterReducer.buildReducer()
       )
    { }

    //public override Reducer<CounterState> createReducer() => CounterReducer.buildReducer();
}

internal class MessageComponent : Component<MessageState> //: AbstractLogic<MessageState>
{
    public MessageComponent() : base(
     //view: buildView,
     //effect: buildEffect(),
     reducer: MessageReducer.buildReducer()
   )
    { }

    //public override Reducer<MessageState> createReducer() => MessageReducer.buildReducer();
}
