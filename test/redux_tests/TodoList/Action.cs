using Message;
using Todo;
using Action = Redux.Basic.Action;

namespace TodoList;

internal enum TodoListAction
{
    initToDos,
    onAdd
}

internal class ToDoListActionCreator
{
    internal static Action initToDos(List<ToDoState> toDos)
    {
        return new Action(TodoListAction.initToDos, toDos);
    }

    internal static Action onAdd()
    {
        return new Action(TodoListAction.onAdd);
    }
}
