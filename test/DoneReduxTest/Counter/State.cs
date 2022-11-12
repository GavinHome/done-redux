using System;
namespace DoneReduxTest.Counter;

[Serializable]
public class CounterState
{
    public int Counter { get; set; } = 0;

    public static CounterState initState()
    {
        var state = new CounterState();
        return state;
    }
}
