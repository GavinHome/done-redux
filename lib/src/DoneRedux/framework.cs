namespace Redux.Framework;

public class Store<T>
{
    private T state;
    private IList<System.Action> listeners;
    private Reducer<T>? reducer;

    public Store(T initState, Reducer<T>? reducer)
    {
        this.state = initState;
        this.listeners = new List<System.Action>();
        this.reducer = reducer;
    }

    public async Task subscribe(System.Action listener)
    {
        await Task.Run(() =>
        {
            listeners.Add(listener);
        });
    }

    public async Task dispatch(Redux.Framework.Action action)
    {
        if (reducer != null)
        {
            this.state = reducer(state, action);

            await Task.Run(() =>
            {
                foreach (var listener in listeners)
                {
                    listener();
                }
            });
        }

        //if (effect != null)
        //{
        //    effect(action, new StoreContext<T>(this));
        //}
    }


    public async Task dispatch(Func<Action> func)
    {
        await this.dispatch(func());
    }


    public async Task<T> getState()
    {
        return await Task.Run(() => this.state);
    }

    /// <summary>
    /// Create Store
    /// </summary>
    /// <typeparam name="T">The type of state.</typeparam>
    /// <param name="initState">The state object instance.</param>
    /// <returns>The store object</returns>
    /// <exception cref="NotImplementedException"></exception>
    public static Store<T> createStore(T initState, Reducer<T> reducer)
    {
        return new Store<T>(initState, reducer);
    }

    /////// Definition of a standard subscription function.
    /////// input a subscriber and output an anti-subscription function.
    ///public delegate void Subscribe(System.Action callback);
}



public class Action
{
    private readonly Object _type;
    private readonly dynamic? _payload;

    public Action(Object type, dynamic? payload)
    {
        _type = type;
        _payload = payload;
    }

    public Action(Object type)
    {
        _type = type;
    }

    public Object Type { get { return _type; } }
    public dynamic? Payload { get { return _payload; } }

    public override bool Equals(object obj)
    {
        return obj != null && Equals(other: obj as Action);
    }

    public bool Equals(Action? other)
    {
        return other != null && _type == other.Type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_type);
    }
}


/// Definition of the standard Reducer.
/// If the Reducer needs to respond to the Action, it returns a new state, otherwise it returns the old state.
public delegate T Reducer<T>(T state, Action action);

/// Definition of the standard Dispatch.
/// Send an "intention".
public delegate dynamic Dispatch(Action action);

/// Interrupt if not null not false
/// bool for sync-functions, interrupted if true
/// Future<void> for async-functions, should always be interrupted.
public delegate dynamic Effect<T>(Action action, Context<T> ctx);

///
public delegate Task SubEffect<T>(Action action, Context<T> ctx);

///  Seen in effect-part
public abstract class Context<T>
{
    /// Get the latest state
    public T? state { get; }

    /// The way to send action, which will be consumed by self, or by broadcast-module and store.
    public abstract Task<dynamic> dispatch(Action action);

    ///// Get BuildContext from the host-widget
    //public BuildContext? context { get; }
}

public class StoreContext<T> : Context<T>
{
    private Store<T> _store;
    public StoreContext(Store<T> store)
    {
        this._store = store;
    }

    //public new T state { get; } = _store.getState();

    public override async Task<dynamic> dispatch(Action action)
    {
        await this._store.dispatch(action);
        return this;
    }
}


public static class ReduxHelper
{
    public static Reducer<T> combineReducers<T>(Dictionary<Object, Reducer<T>> map)
    {
        if (map == null || !map.Any())
        {
            return null;
        }
        else
        {
            return (T state, Action action) =>
            {
                var fn = map.FirstOrDefault(entry => action.Type.Equals(entry.Key)).Value;
                if (fn != null) { return fn(state, action); }
                else return state;
            };
        }
    }

    readonly static Object _SUB_EFFECT_RETURN_NULL = new Object();

    
    public static Effect<T>? combineEffects<T>(Dictionary<object, SubEffect<T>> map) => (map == null || !map.Any())
        ? null : (Action action, Context<T> ctx) =>
        {
            SubEffect<T> subEffect = map.FirstOrDefault(entry => action.Type.Equals(entry.Key)).Value;
            if (subEffect != null)
            {
                return subEffect.Invoke(action, ctx) ?? _SUB_EFFECT_RETURN_NULL;
            }

            ////kip-lifecycle-actions
            //if (action.Type is Lifecycle)
            //{
            //    return _SUB_EFFECT_RETURN_NULL;
            //}

            ////no subEffect
            return null;
        };
}