using Redux;
namespace Counter;

internal class CounterReducer
{
    internal static Reducer<CounterState> buildReducer()
    {
        var map = new Dictionary<Object, Reducer<CounterState>>();
        map.Add(CounterAction.add, _add);
        map.Add(CounterAction.minus, _minus);
        return Converter.asReducers<CounterState>(map);
    }

    private static CounterState _minus(CounterState state, Redux.Action action)
    {
        CounterState? newState = state.Clone(); //clone
        newState.Count -= action.Payload;
        return newState;
    }

    private static CounterState _add(CounterState state, Redux.Action action)
    {
        CounterState? newState = state.Clone(); //clone
        newState.Count += action.Payload;
        return newState;
    }
}
