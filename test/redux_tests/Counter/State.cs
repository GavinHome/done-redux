namespace Counter;

[Serializable]
internal class CounterState
{
    public int Count { get; set; } = 0;

    public static CounterState initState()
    {
        var state = new CounterState();
        return state;
    }
}
