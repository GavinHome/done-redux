using System;

namespace Counter;
using Action = Redux.Basic.Action;

internal enum CounterAction
{
    add,
    minus,
    ////onCompute,
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

    ////internal static Action onCompute()
    ////{
    ////    return new Action(CounterAction.onCompute);
    ////}
}

////internal class CounterActionCreators
////{

////    internal Action add(int payload)
////    {
////        return new Action(CounterAction.add, payload);
////    }

////    internal Action minus(int payload)
////    {
////        return new Action(CounterAction.minus, payload);
////    }
////}
