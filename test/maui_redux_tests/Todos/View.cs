using System;
using Redux;

namespace example.Pages.Todos;

public class TodoListView
{
    internal static dynamic buildView(TodoListState state, Dispatch dispatch)
    {
        //dispatch(ToDoListActionCreator.initToDos(new List<TodoComponent.ToDoState>()));
        return new
        {
            Content = new
            {
                Children = new
                {
                    Body = new
                    {

                        Text = "Welcome to Todos Redux Page!"
                    }
                }
            }
        };
    }
}