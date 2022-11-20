using System;
using Redux;

namespace example.Pages.Todos;

public class TodoListView
{
    internal static IView buildView(TodoListState state, Dispatch dispatch)
    {
        //return new Page();
        return new ContentPage
        {
            Content = new VerticalStackLayout
            {
                Children =
                {
                    new Label {
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Text = "Welcome to Todos Redux Page!"
                    }
                }
            }
        };
    }
}

public class Page : ContentPage
{
    public Page()
    {
        Content = new ToDoListPage().buildPage(null);
        //Content = new VerticalStackLayout
        //{
        //    Children = {
        //        new Label {
        //            HorizontalOptions = LayoutOptions.Center,
        //            VerticalOptions = LayoutOptions.Center,
        //            Text = "Welcome to Todos Page!"
        //        }
        //    }
        //};
    }
}

