namespace Todo;

internal class ToDoComponent : Redux.Component<ToDoState>
{
    public ToDoComponent() : base(
          //view: buildView,
          //effect: buildEffect(),
          reducer: TodoReducer.buildReducer()
        )
    {
    }
}
