using Redux.Basic;
using Redux.Utils;

namespace Message;
using Action = Redux.Basic.Action;

internal class MessageReducer
{
    internal static Reducer<MessageState> buildReducer()
    {
        var map = new Dictionary<Object, Reducer<MessageState>>();
        map.Add(MessageAction.modify, _modify);
        return Redux.Helper.asReducers<MessageState>(map);
    }

    private static MessageState _modify(MessageState state, Action action)
    {
        MessageState? newState = state.Clone(); //clone
        newState.Id = action.Payload.Id;
        newState.Content = action.Payload.Content;
        return newState;
    }
}
