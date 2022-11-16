using Redux.Effect;
using Action = Redux.Basic.Action;

namespace Counter;

public class CounterEffect
{
    internal static Effect<CounterState>? buildEffect()
    {
        var map = new Dictionary<object, SubEffect<CounterState>>();
        map.Add(CounterAction.onCompute, _onCompute);
        return Redux.Helper.combineEffects(map);
    }

    private static async Task _onCompute(Action action, Context<CounterState> ctx)
    {
        await ctx.Dispatch(CounterActionCreator.add(1));
        await ctx.Dispatch(CounterActionCreator.minus(2));
    }
}
