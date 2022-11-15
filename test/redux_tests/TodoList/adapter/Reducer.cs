using Redux.Basic;
using Redux.Utils;
using System.Linq;
using System.Xml;
using Todo;
using TodoList;

namespace TodoList;
using Action = Redux.Basic.Action;

internal class TodoListAdapterReducer
{
    internal static Reducer<TodoListState> buildReducer()
    {
        var map = new Dictionary<Object, Reducer<TodoListState>>();
        map.Add(TodoListAdapterAction.add, _add);
        map.Add(TodoListAdapterAction.remove, _remove);
        return Redux.Helper.asReducers<TodoListState>(map);
    }

    private static TodoListState _add(TodoListState state, Action action)
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

    private static TodoListState _remove(TodoListState state, Action action)
    {
        String? id = action.Payload;
        List<ToDoState> list = state.toDos?.ToList() ?? new List<ToDoState>();
        list.RemoveAll(x => x.Id == id);
        TodoListState? newState = state.Clone(); //clone
        newState.toDos = list;
        return newState;
    }
}
