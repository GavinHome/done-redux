namespace example.Pages.Counter;

internal class CounterPage : Redux.Maui.Page<Counter.CounterState, dynamic>
{
    public CounterPage() : base(
         initState: (p) => CounterState.initState(),
         view: CounterView.buildView,
         //effect: buildEffect(),
         reducer: Counter.CounterReducer.buildReducer(),
         dependencies: null
       )
    { }
}