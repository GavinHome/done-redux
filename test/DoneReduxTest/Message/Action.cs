using System;
using Action = Redux.Basic.Action;

namespace Message;

internal enum MessageAction
{
    modify,
}

internal class MessageActionCreator
{
    internal static Action modify(dynamic payload)
    {
        return new Action(MessageAction.modify, payload);
    }
}
