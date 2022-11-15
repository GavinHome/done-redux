using Redux.Adapter;
using Redux.Dependencies.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TodoList;

internal static class Adapter
{
    public static FlowAdapter<TodoListState> adapter => new FlowAdapter<TodoListState>(
        reducer: TodoListAdapterReducer.buildReducer(),
        view: (TodoListState state) => new DependentArray<TodoListState>(
            length: state.toDos?.Count ?? 0,
            builder: (int index) =>
            {
                return new ToDoConnector(index: index).add(new Todo.ToDoComponent());
            })
    );
}
