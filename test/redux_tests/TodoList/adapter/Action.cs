using Message;
using Todo;
using Action = Redux.Basic.Action;

namespace TodoList;

internal enum TodoListAdapterAction
{
    add,
    remove
}

internal class TodoListAdapterActionCreator
{
    internal static Action add(ToDoState payload)
    {
        return new Action(TodoListAdapterAction.add, payload);
    }

    internal static Action remove(String id)
    {
        return new Action(TodoListAdapterAction.remove, id);
    }
}
