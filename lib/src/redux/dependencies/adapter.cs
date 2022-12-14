using Redux.Dependencies;

namespace Redux.Adapter;

public interface AbstractAdapterBuilder<T>
{    
}

public interface AbstractAdapter<T> : AbstractAdapterBuilder<T> { }

public delegate DependentArray<T> FlowAdapterView<T>(T state);

public class FlowAdapter<T> : Logic<T>, AbstractAdapter<T>
{
    private FlowDependencies<T> _flowDependencies;

    public FlowAdapter(Reducer<T> reducer, FlowAdapterView<T> view) : base(reducer, null)
    {
        _flowDependencies = new FlowDependencies<T>(view);
    }

    protected override Reducer<T> protectedDependenciesReducer =>_flowDependencies.createReducer();
}


