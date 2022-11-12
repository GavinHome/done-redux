using System;
namespace DoneReduxTest.Counter;
using Action = Redux.Framework.Action;

internal enum CounterAction
{
    add,
    minus,
}

internal class CounterActionCreator
{

    internal static Action add(int payload)
    {
        return new Action(CounterAction.add, payload);
    }

    internal static Action minus(int payload)
    {
        return new Action(CounterAction.minus, payload);
    }
}
