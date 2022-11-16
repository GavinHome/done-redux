using System;
using Action = Redux.Basic.Action;

namespace Message;

internal enum MessageAction
{
    modify,
}

internal class MessageActionCreator
{
    internal static Action modify(int id, string content)
    {
        return new Action(MessageAction.modify, new MessageState(id, content));
    }
}
