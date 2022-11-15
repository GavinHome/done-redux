using Redux.Basic;
using Redux.Utils;
using Todo;

namespace TodoList;
using Action = Redux.Basic.Action;

internal class TodoListReducer
{
    internal static Reducer<TodoListState> buildReducer()
    {
        var map = new Dictionary<Object, Reducer<TodoListState>>();
        map.Add(TodoListAction.initToDos, _initToDos);
        return Redux.Helper.asReducers<TodoListState>(map);
    }

    private static TodoListState _initToDos(TodoListState state, Action action)
    {
        List<ToDoState> toDos = action.Payload ?? new List<ToDoState>();
        TodoListState? newState = state.Clone(); //clone
        newState.toDos = toDos;
        return newState;
    }
}
