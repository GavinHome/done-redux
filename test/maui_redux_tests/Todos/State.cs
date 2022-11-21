using example.Pages.Todos.TodoComponent;

namespace example.Pages.Todos;

[Serializable]
internal class TodoListState
{
    public List<ToDoState> toDos { get; set; }

    internal static TodoListState initState(IDictionary<String,dynamic> args)
    {
        //just demo, do nothing here...
        return new TodoListState();
    }
}

