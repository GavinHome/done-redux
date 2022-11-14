using Redux.Basic;
using Redux.Dependencies.Basic;
using Redux.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return (T state, Redux.Basic.Action action, bool isStateCopied) =>
            {
                P props = Get(state);
                if (props == null)
                {
                    return state;
                }
                P newProps = reducer(props, action);
                bool hasChanged = !EqualityComparer<P>.Default.Equals(newProps, props); //newProps != props
                T copy = (hasChanged && !isStateCopied) ? _clone<T>(state) : state;
                if (hasChanged)
                {
                    Set(copy, newProps);
                }

                return copy;
            };
        }
    }

    /// how to clone an object
    T? _clone<T>(T state)
    {
        if (state is ICloneable || state is Object || state is List<Object> || state is Dictionary<String, dynamic>)
        {
            return state.Clone();
        }
        else if (state == null)
        {
            return default(T);
        }
        else
        {
            throw new ArgumentException($"Could not clone this state of type {typeof(T)}");
        }
    }
}

public abstract class AbstractLogic<T>
{
    /// To create a reducer<T>
    public abstract Reducer<T> createReducer();
}

public class ConnOp<T, P> : MutableConn<T, P>
{
    private System.Func<T, P> _getter;
    private System.Action<T, P> _setter;

    public ConnOp(System.Func<T, P> get, System.Action<T, P> set)
    {
        _getter = get;
        _setter = set;
    }

    public ConnOp() { }

    public override P Get(T state) => _getter(state);

    public override void Set(T state, P subState) => _setter(state, subState);

    public Dependent<T> add(AbstractLogic<P> logic) =>
       Redux.Dependencies.Creator.createDependent<T, P>(this, logic);
}
