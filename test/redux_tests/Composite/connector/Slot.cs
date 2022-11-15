using Counter;
using Message;
using Redux.Basic;
using Redux.Component;
using Redux.Connector;
using Redux.Dependencies;
using System.ComponentModel;
using Todo;

namespace Composite;

internal class CounterConnector : ConnOp<CompositeState, CounterState>
{
    public override CounterState Get(CompositeState state)
    {
        return state.Counter;
    }

    public override void Set(CompositeState state, CounterState subState)
    {
        state.Counter = subState;
    }
}

internal class MessageConnector : ConnOp<CompositeState, MessageState>
{
    public override MessageState Get(CompositeState state)
    {
        return state.Message;
    }

    public override void Set(CompositeState state, MessageState subState)
    {
        state.Message = subState;
    }
}
