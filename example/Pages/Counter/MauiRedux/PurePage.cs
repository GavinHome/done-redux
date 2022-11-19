using example.Pages.Counter.FluentView;
namespace example.Pages.Counter;

internal class PureCounterPage : Redux.Maui.Page<Counter.CounterState, dynamic>
{
    public PureCounterPage() : base(
         initState: (p) => CounterState.initState(),
         view: (s, p) => new PureView(),
         //effect: buildEffect(),
         reducer: Counter.CounterReducer.buildReducer(),
         dependencies: null
       )
    { }
}