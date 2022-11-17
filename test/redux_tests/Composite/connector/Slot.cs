namespace Composite;

internal class CounterConnector : Redux.Connector.ConnOp<CompositeState, Counter.CounterState>
{
    public override Counter.CounterState Get(CompositeState state)
    {
        return state.Counter;
    }

    public override void Set(CompositeState state, Counter.CounterState subState)
    {
        state.Counter = subState;
    }
}

internal class MessageConnector : Redux.Connector.ConnOp<CompositeState, Message.MessageState>
{
    public override Message.MessageState Get(CompositeState state)
    {
        return state.Message;
    }

    public override void Set(CompositeState state, Message.MessageState subState)
    {
        state.Message = subState;
    }
}
