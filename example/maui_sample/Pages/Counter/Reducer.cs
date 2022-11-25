using Redux;
namespace example.Pages.Counter;

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
        CounterState newState = state.Clone(); //clone
        newState.Count -= action.Payload;
        newState.Message = buildMessageContent(newState.Count);
        return newState;
    }

    private static CounterState _add(CounterState state, Redux.Action action)
    {
        CounterState newState = state.Clone(); //clone
        newState.Count += action.Payload;
        newState.Message = buildMessageContent(newState.Count);
        return newState;
    }

    private static string buildMessageContent(int count)
    {
        if (count == 0)
        {
            return "Click me";
        }
        else
        {
            return $"Clicked {count} {(count == 1 ? "time" : "times")}";
        }
    }
}
