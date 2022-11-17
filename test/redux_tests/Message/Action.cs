namespace Message;

internal enum MessageAction
{
    modify,
}

internal class MessageActionCreator
{
    internal static Redux.Action modify(int id, string content)
    {
        return new Redux.Action(MessageAction.modify, new MessageState(id, content));
    }
}
