using Todo;

namespace TodoList;

[Serializable]
internal class TodoListState
{
    public List<ToDoState>? toDos { get; set; }

    internal static TodoListState initState()
    {
        //just demo, do nothing here...
        return new TodoListState();
    }
}
