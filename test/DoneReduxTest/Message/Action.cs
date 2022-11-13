using System;
namespace DoneReduxTest.Message;
using Action = Redux.Framework.Action;

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
