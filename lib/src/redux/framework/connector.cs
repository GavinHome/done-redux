using Redux.Dependencies;

namespace Redux.Connector;

public abstract class MutableConn<T, P> : AbstractConnector<T, P>
{
    public MutableConn() { }

    public abstract void Set(T state, P subState);

    public override SubReducer<T> subReducer(Reducer<P> reducer)
    {
        if (reducer == null)
        {
            return null;
        }
        else
        {
            return (state, action, isStateCopied) =>
            {
                P props = Get(state);
                if (props == null)
                {
                    return state;
                }
                P newProps = reducer(props, action);
                bool hasChanged = !EqualityComparer<P>.Default.Equals(newProps, props); //newProps != props
                T copy = hasChanged && !isStateCopied ? _clone(state) : state;
                if (hasChanged)
                {
                    Set(copy, newProps);
                }

                return copy;
            };
        }
    }

    /// how to clone an object
    T _clone<T>(T state)
    {
        if (state is ICloneable || state is object || state is List<object> || state is Dictionary<string, dynamic>)
        {
            return state.Clone();
        }
        else if (state == null)
        {
            return default;
        }
        else
        {
            throw new ArgumentException($"Could not clone this state of type {typeof(T)}");
        }
    }
}

public class ConnOp<T, P> : MutableConn<T, P>
{
    private Func<T, P>? _getter;
    private Action<T, P>? _setter;

    public ConnOp(Func<T, P> getter, Action<T, P> setter)
    {
        _getter = getter;
        _setter = setter;
    }

    public ConnOp() { }

    public override P Get(T state) => _getter(state);

    public override void Set(T state, P subState) => _setter(state, subState);

    public Dependent<T> add(AbstractLogic<P> logic) =>
       Redux.DependentCreator.createDependent(this, logic);
}

public abstract class ImmutableConn<T, P> : AbstractConnector<T, P>
{
    public ImmutableConn() { }

    public abstract T Set(T state, P subState);

    public override SubReducer<T> subReducer(Reducer<P> reducer)
    {
        return reducer == null
            ? null
            : (T state, Redux.Action action, bool isStateCopied) =>
            {
                P props = Get(state);
                if (props == null)
                {
                    return state;
                }

                P newProps = reducer(props, action);
                bool hasChanged = !EqualityComparer<P>.Default.Equals(newProps, props); //newProps != props
                if (hasChanged)
                {
                    T result = Set(state, newProps);
                    return result;
                }

                return state;
            };
    }

    public Dependent<T> add(AbstractLogic<P> logic) =>
       Redux.DependentCreator.createDependent(this, logic);
}

public class NoneConn<T> : ImmutableConn<T, T>
{
    public NoneConn() { }

    public override T Get(T state) => state;

    public override T Set(T state, T subState) => subState;
}
