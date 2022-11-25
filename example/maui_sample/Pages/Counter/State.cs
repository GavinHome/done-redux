namespace example.Pages.Counter;

[Serializable]
public class CounterState
{
    public int Count { get; set; } = 0;
    public string Message { get; set; } = "Click me";

    public static CounterState initState()
    {
        var state = new CounterState();
        return state;
    }
}
