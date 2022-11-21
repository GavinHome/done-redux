using example.Pages.Todos.TodoComponent;

namespace example.Pages.Todos.FlowAdapter;

internal static class Adapter
{
    public static Redux.Adapter.FlowAdapter<TodoListState> adapter => new Redux.Adapter.FlowAdapter<TodoListState>(
        reducer: TodoListAdapterReducer.buildReducer(),
        view: (TodoListState state) => new Redux.DependentArray<TodoListState>(
            length: state.toDos?.Count ?? 0,
            builder: (int index) =>
            {
                return new ToDoConnector(index: index).add(new ToDoComponent());
            })
    );
}

