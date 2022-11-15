using Redux.Component;

namespace Todo;

internal class ToDoComponent : Component<ToDoState>
{
    public ToDoComponent() : base(
          //view: buildView,
          //effect: buildEffect(),
          reducer: TodoReducer.buildReducer()
        )
    {
    }
}
