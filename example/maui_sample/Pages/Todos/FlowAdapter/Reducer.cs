using example.Pages.Todos.TodoComponent;
using Redux;

namespace example.Pages.Todos.FlowAdapter;


internal class TodoListAdapterReducer
{
    internal static Redux.Reducer<TodoListState> buildReducer()
    {
        var map = new Dictionary<Object, Redux.Reducer<TodoListState>>();
        map.Add(TodoListAdapterAction.add, _add);
        map.Add(TodoListAdapterAction.remove, _remove);
        return Redux.Converter.asReducers<TodoListState>(map);
    }

    private static TodoListState _add(TodoListState state, Redux.Action action)
    {
        ToDoState? toDo = action.Payload;
        if (toDo != null)
        {
            List<ToDoState> list = state.toDos?.ToList() ?? new List<ToDoState>();
            list.Add(toDo);
            TodoListState? newState = state.Clone(); //clone
            newState.toDos = list;
            return newState;
        }

        return state;
    }

    private static TodoListState _remove(TodoListState state, Redux.Action action)
    {
        String? id = action.Payload;
        List<ToDoState> list = state.toDos?.ToList() ?? new List<ToDoState>();
        list.RemoveAll(x => x.Id == id);
        TodoListState? newState = state.Clone(); //clone
        newState.toDos = list;
        return newState;
    }
}
