using example.Pages.Counter.FluentView;
namespace example.Pages.Counter;

internal class FluentCounterPage : Redux.Maui.Page<Counter.CounterState, dynamic>
{
    public FluentCounterPage() : base(
         initState: (p) => CounterState.initState(),
         view: CounterView.buildView,         
         //effect: buildEffect(),
         reducer: Counter.CounterReducer.buildReducer(),
         dependencies: null
       )
    { }
}