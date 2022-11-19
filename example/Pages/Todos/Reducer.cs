using example.Pages.Todos.TodoComponent;
using Redux;

namespace example.Pages.Todos;

internal class TodoListReducer
{
    internal static Reducer<TodoListState> buildReducer()
    {
        var map = new Dictionary<Object, Reducer<TodoListState>>();
        map.Add(TodoListAction.initToDos, _initToDos);
        return Redux.Converter.asReducers<TodoListState>(map);
    }

    private static TodoListState _initToDos(TodoListState state, Redux.Action action)
    {
        List<ToDoState> toDos = action.Payload ?? new List<ToDoState>();
        TodoListState? newState = state.Clone(); //clone
        newState.toDos = toDos;
        return newState;
    }
}
