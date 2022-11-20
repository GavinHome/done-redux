namespace Redux.Dependencies;

public abstract class AbstractLogic<T>
{
    /// To create a reducer<T>
    public abstract Reducer<T> createReducer();

    /// Find a dependent by name
    //public abstract Dependent<T> slot(String name);

    /// To create each instance's dispatch
    /// Dispatch is the most important api for users which is provided by framework
    //public abstract Dispatch createDispatch(
    //  Dispatch effectDispatch,
    //  Dispatch nextDispatch
    //  //ContextSys<T> ctx,
    //);
}

/// Four parts
/// 1. Reducer & ReducerFilter
/// 2. Effect
/// 3. Dependencies
/// 4. Key
public abstract class Logic<T> : AbstractLogic<T>
{
    Reducer<T> _reducer;
    Dependencies<T>? _dependencies;

    public Logic(Reducer<T> reducer, Dependencies<T>? dependencies)
    {
        _reducer = reducer;
        _dependencies = dependencies?.trim();
    }

    protected virtual Reducer<T> protectedReducer => _reducer;

    protected virtual Dependencies<T>? protectedDependencies => _dependencies;

    protected virtual Reducer<T> protectedDependenciesReducer => protectedDependencies?.createReducer();

    public override Reducer<T> createReducer() => Redux.ReducerCreator.combineReducers(new List<Reducer<T>>() { protectedReducer, protectedDependenciesReducer });
}
