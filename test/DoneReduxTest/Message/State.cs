using System;
using Redux.Framework;

namespace DoneReduxTest.Message;

[Serializable]
public class MessageState
{
    public int id { get; set; }
    public string content { get; set; }

    public MessageState(int id, string content)
    {
        this.id = id;
        this.content = content;
    }

    public static MessageState initState()
    {
        var state = new MessageState(0, "test");
        return state;
    }
}
