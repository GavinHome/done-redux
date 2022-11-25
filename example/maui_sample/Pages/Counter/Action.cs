namespace example.Pages.Counter;
internal enum CounterAction
{
    add,
    minus,
}

internal class CounterActionCreator
{
    internal static Redux.Action add(int payload)
    {
        return new Redux.Action(CounterAction.add, payload);
    }

    internal static Redux.Action minus(int payload)
    {
        return new Redux.Action(CounterAction.minus, payload);
    }
}
