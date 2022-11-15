using Message;
using Action = Redux.Basic.Action;

namespace Todo;

internal enum ToDoAction
{
    //onEdit,   //effect
    //onRemove, //effect
    edit,
    done
}

internal class ToDoActionCreator
{
    //internal static Action onEditAction(String id)
    //{
    //    return new Action(ToDoAction.onEdit, payload: id);
    //}

    internal static Action edit(ToDoState toDo)
    {
        return new Action(ToDoAction.edit, payload: toDo);
    }

    internal static Action done(String id)
    {
        return new Action(ToDoAction.done, payload: id);
    }

    //internal static Action onRemoveAction(String id)
    //{
    //    return new Action(ToDoAction.onRemove, payload: id);
    //}
}
