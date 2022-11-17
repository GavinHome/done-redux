namespace Todo;

internal enum ToDoAction
{
    edit,
    done
}

internal class ToDoActionCreator
{
    internal static Redux.Action edit(ToDoState toDo)
    {
        return new Redux.Action(ToDoAction.edit, payload: toDo);
    }

    internal static Redux.Action done(String id)
    {
        return new Redux.Action(ToDoAction.done, payload: id);
    }
}
