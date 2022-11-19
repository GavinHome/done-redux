using example.Pages.Todos.TodoComponent;

namespace example.Pages.Todos.FlowAdapter;


internal class ToDoConnector : Redux.Connector.ConnOp<TodoListState, ToDoState>
{
    private int index;

    public ToDoConnector(int index)
    {
        this.index = index;
    }

    public override ToDoState Get(TodoListState state)
    {
        if (index >= state.toDos?.Count || state.toDos == null)
        {
            return null;
        }

        return state.toDos[index];
    }

    public override void Set(TodoListState state, ToDoState subState)
    {
        if (state.toDos != null)
        {
            state.toDos[index] = subState;
        }
    }
}
