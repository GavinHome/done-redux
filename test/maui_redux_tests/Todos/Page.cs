using Redux;
using Redux.Maui;

namespace example.Pages.Todos;

internal class ToDoListPage: Page<TodoListState, IDictionary<String, dynamic>> {

    public ToDoListPage()
      : base(initState: TodoListState.initState,
          reducer: TodoListReducer.buildReducer(),
          view: TodoListView.buildView,
          dependencies: null)
    { }
}
