using System;
using Redux.Framework;
using Redux.Utils;
using Action = Redux.Framework.Action;

namespace DoneReduxTest.Counter;

public class CounterEffect
{
	internal static Effect<CounterState>? buildEffect()
    {
        var map = new Dictionary<object, SubEffect<CounterState>>();
        map.Add(CounterAction.onCompute, _onCompute);
        return ReduxHelper.combineEffects(map);
    }

    private static async Task _onCompute(Action action, Context<CounterState> ctx)
    {
        await ctx.dispatch(CounterActionCreator.add(1));
        await ctx.dispatch(CounterActionCreator.minus(2));
    }
}


