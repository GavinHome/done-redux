using example.Pages.Todos.TodoComponent;

namespace example.Pages.Todos.FlowAdapter;

internal enum TodoListAdapterAction
{
    add,
    remove
}

internal class TodoListAdapterActionCreator
{
    internal static Redux.Action add(ToDoState payload)
    {
        return new Redux.Action(TodoListAdapterAction.add, payload);
    }

    internal static Redux.Action remove(String id)
    {
        return new Redux.Action(TodoListAdapterAction.remove, id);
    }
}
