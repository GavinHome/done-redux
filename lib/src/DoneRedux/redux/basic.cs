using static System.Formats.Asn1.AsnWriter;

namespace Redux.Basic;

public class Action
{
    private readonly object _type;
    private readonly dynamic? _payload;

    public Action(object type, dynamic? payload)
    {
        _type = type;
        _payload = payload;
    }

    public Action(object type)
    {
        _type = type;
    }

    public object Type { get { return _type; } }
    public dynamic? Payload { get { return _payload; } }

    public override bool Equals(object obj) => obj != null && Equals(other: obj as Action);

    public bool Equals(Action? other) => other != null && _type == other.Type;

    public override int GetHashCode() => HashCode.Combine(_type);
}

/// Definition of the standard Reducer.
/// If the Reducer needs to respond to the Action, it returns a new state, otherwise it returns the old state.
public delegate T Reducer<T>(T state, Action action);

/// Definition of the standard Dispatch.
/// Send an "intention".
public delegate void Dispatch(Action action);

/// Definition of a standard subscription function.
/// input a subscriber and output an anti-subscription function.
public delegate void Subscribe(System.Action callback);

/////// Definition of ReplaceReducer
//public delegate void ReplaceReducer<T>(Reducer<T> reducer);

/////// Definition of the standard observable flow.
//public delegate Stream Observable<T>();

/// Definition of the function type that returns type R.
public delegate R Get<R>();

/// Definition of synthesizable functions.
//typedef Composable<T> = T Function(T next);
public delegate T Composable<T>(T next);

/// Definition of the standard Middleware.
public delegate Composable<Dispatch> Middleware<T>(Dispatch dispatch, Get<T> getState);


/// Definition of the standard Store.
public class Store<T>
{
    private T _state;
    private IList<System.Action> _listeners;
    private Reducer<T> _reducer;

    public Get<T> GetState => () => _state;
    public Dispatch Dispatch { get; set; }

    public Subscribe? Subscribe { get; set; }
    ////public Observable<T>? Observable { get; set; }
    ////public ReplaceReducer<T>? ReplaceReducer { get; set; }
    ////public Task<dynamic>? Teardown { get; set; }

    public Store(T initState, Reducer<T> reducer)
    {
        _state = initState;
        _listeners = new List<System.Action>();
        _reducer = reducer;

        Dispatch = (action) =>
        {
            if (this._reducer != null)
            {
                _state = reducer(_state, action);

                foreach (var listener in _listeners)
                {
                    listener();
                };
            }
        };

        Subscribe = (listener) => _listeners.Add(listener);
    }
}

/// Create a store definition
public delegate Store<T> StoreCreator<T>(
    T preloadedState,
    Reducer<T> reducer
);

/// Definition of Enhanced creating a store
public delegate StoreCreator<T> StoreEnhancer<T>(StoreCreator<T> creator);

/// Definition of SubReducer
/// [isStateCopied] is Used to optimize execution performance.
/// Ensure that a T will be cloned at most once during the entire process.
public delegate T SubReducer<T>(T state, Action action, bool isStateCopied);
