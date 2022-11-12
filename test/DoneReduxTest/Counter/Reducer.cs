using System;
using Redux.Framework;
using Redux.Utils;

namespace DoneReduxTest.Counter;
using Action = Redux.Framework.Action;

public class CounterReducer
{
    internal static Reducer<CounterState> buildReducer()
    {
        var map = new Dictionary<Object, Reducer<CounterState>>();
        map.Add(CounterAction.add, _add);
        map.Add(CounterAction.minus, _minus);
        return Redux.Framework.ReduxHelper.combineReducers<CounterState>(map);
    }

    private static CounterState _minus(CounterState state, Action action)
    {
        CounterState? newState = state.Clone(); //clone
        newState.Counter -= action.Payload;
        return newState;
    }

    private static CounterState _add(CounterState state, Action action)
    {
        CounterState? newState = state.Clone(); //clone
        newState.Counter += action.Payload;
        return newState;
    }
}
