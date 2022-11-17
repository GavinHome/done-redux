using Counter;
using Message;

namespace Composite;

[Serializable]
internal class CompositeState
{
    public CounterState Counter { get; set; }
    public MessageState Message { get; set; }

    public CompositeState()
    {
        Counter = new Counter.CounterState();
        Message = new MessageState();
    }

    public static CompositeState initState()
    {
        return new CompositeState()
        {
            Counter = CounterState.initState(),
            Message = MessageState.initState()
        };
    }
}
