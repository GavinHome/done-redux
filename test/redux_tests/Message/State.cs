namespace Message;

[Serializable]
internal class MessageState
{
    public int Id { get; set; } = 0;
    public string Content { get; set; } = string.Empty;

    public MessageState() { }

    public MessageState(int id, string content)
    {
        this.Id = id;
        this.Content = content;
    }

    public static MessageState initState()
    {
        var state = new MessageState(0, "test");
        return state;
    }
}
