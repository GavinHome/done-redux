﻿using example.Pages.Todos.TodoComponent;

namespace example.Pages.Todos;

internal enum TodoListAction
{
    initToDos,
    onAdd
}

internal class ToDoListActionCreator
{
    internal static Redux.Action initToDos(List<ToDoState> toDos)
    {
        return new Redux.Action(TodoListAction.initToDos, toDos);
    }

    internal static Redux.Action onAdd()
    {
        return new Redux.Action(TodoListAction.onAdd);
    }
}
