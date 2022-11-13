using System;
using DoneReduxTest.Counter;
using Redux.Framework;
using Redux.Utils;

namespace DoneReduxTest.Message;
using Action = Redux.Framework.Action;

public class MessageReducer
{
    internal static Reducer<MessageState> buildReducer()
    {
        var map = new Dictionary<Object, Reducer<MessageState>>();
        map.Add(MessageAction.modify, _modify);
        return Redux.Framework.ReduxHelper.asReducers<MessageState>(map);
    }

    private static MessageState _modify(MessageState state, Action action)
    {
        MessageState? newState = state.Clone(); //clone
        newState.id = action.Payload.id;
        newState.content = action.Payload.content;
        return newState;
    }
}
