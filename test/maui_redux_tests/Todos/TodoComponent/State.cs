namespace example.Pages.Todos.TodoComponent;

[Serializable]
internal class ToDoState
{
    public String Id { get; set; }
    public String Title { get; set; }
    public String Desc { get; set; }
    public bool IsDone { get; set; }

    public ToDoState(String id, String title, String desc, bool isDone)
    {
        this.Id = id;
        this.Title = title;
        this.Desc = desc;
        this.IsDone = isDone;
    }

    public String toString()
    {
        return $"ToDoState id: {Id}, title: {Title}, desc: {Desc}, isDone: {IsDone}";
    }
}
