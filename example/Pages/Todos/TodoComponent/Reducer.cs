using Redux;

namespace example.Pages.Todos.TodoComponent;

internal class TodoReducer
{
    internal static Reducer<ToDoState> buildReducer()
    {
        var map = new Dictionary<Object, Reducer<ToDoState>>();
        map.Add(ToDoAction.edit, _edit);
        map.Add(ToDoAction.done, _markDone);
        return Redux.Converter.asReducers<ToDoState>(map);
    }

    private static ToDoState _edit(ToDoState state, Redux.Action action)
    {
        ToDoState? toDo = action.Payload;
        if (state.Id == toDo.Id)
        {
            ToDoState? newState = state.Clone(); //clone
            newState.Title = toDo.Title;
            newState.Desc = toDo.Desc;
            return newState;
        }

        return state;
    }

    private static ToDoState _markDone(ToDoState state, Redux.Action action)
    {
        String? id = action.Payload;
        if (state.Id == id)
        {
            ToDoState? newState = state.Clone(); //clone
            newState.IsDone = !state.IsDone;
            return newState;
        }

        return state;
    }
}
