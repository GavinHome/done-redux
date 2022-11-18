namespace Todo;

internal class ToDoComponent : Redux.AbstractComponent<ToDoState>
{
    public ToDoComponent() : base(
          //effect: buildEffect(),
          reducer: TodoReducer.buildReducer()
        )
    {
    }
}
