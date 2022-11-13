﻿using System.Reflection;
using System.Xml.Linq;
using DoneRedux;
using DoneRedux.utils;

namespace Redux.Framework;

public class Store<T>
{
    private T state;
    private IList<System.Action> listeners;
    private Reducer<T>? reducer;
    private Dispatch _dispatch;
    public Get<T> getState => () => this.state;

    public Store(T initState, Reducer<T>? reducer)
    {
        this.state = initState;
        this.listeners = new List<System.Action>();
        this.reducer = reducer;

        _dispatch = (Action action) =>
        {
            if (this.reducer != null)
            {
                this.state = reducer(this.state, action);

                foreach (var listener in this.listeners)
                {
                    listener();
                };
            }
        };
    }

    public Subscribe subscribe => (System.Action listener) =>
    {
        listeners.Add(listener);
    };

    public Dispatch dispatch
    {
        get => _dispatch;
        set => _dispatch = value;

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

    /// create a store with enhancer
    public static Store<T> createStore(T preloadedState, Reducer<T> reducer, StoreEnhancer<T> enhancer)
    {
        return enhancer != null ? enhancer(createStore)(preloadedState, reducer) : createStore(preloadedState, reducer);
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
public delegate void Dispatch(Action action);

/// Definition of a standard subscription function.
/// input a subscriber and output an anti-subscription function.
//typedef Subscribe = void Function() Function(void Function() callback); 
public delegate void Subscribe(System.Action callback);

/// Definition of the function type that returns type R.
public delegate R Get<R>();

/// Definition of synthesizable functions.
//typedef Composable<T> = T Function(T next);
public delegate T Composable<T>(T next);

/// Definition of the standard Middleware.
//typedef Middleware<T> = Composable<Dispatch> Function({
//    Dispatch dispatch,
//    Get< T > getState,
//}); 
public delegate Composable<Dispatch> Middleware<T>(
    Dispatch dispatch,
    Get<T> getState
);

/// Create a store definition
public delegate Store<T> StoreCreator<T>(T preloadedState, Reducer<T> reducer);

/// Definition of Enhanced creating a store
//typedef StoreEnhancer<T> = StoreCreator<T> Function(StoreCreator<T> creator);
public delegate StoreCreator<T> StoreEnhancer<T>(StoreCreator<T> creator);

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
        this._store.dispatch(action);
        return this;
    }
}


public static class ReduxHelper
{
    public static Reducer<T> asReducers<T>(Dictionary<Object, Reducer<T>> map)
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


    //public static Reducer<T> combineReducers<T>(Dictionary<string, dynamic> map)
    //{
    //    if (map == null || !map.Any())
    //    {
    //        return null;
    //    }
    //    else
    //    {
    //        return (T state, Action action) =>
    //        {
    //            T nextState = state;
    //            foreach (var group in map)
    //            {
    //                string? key = group.Key as string;
    //                if (!string.IsNullOrEmpty(key))
    //                {

    //                    Type entityType = typeof(T);
    //                    PropertyInfo? subStateProperty = entityType.GetProperty(key);
    //                    Type? subStateType = subStateProperty?.PropertyType;
    //                    var subStateValue = subStateProperty?.GetValue(state);

    //                    if(subStateProperty != null && subStateType != null)
    //                    {
    //                        var subReducer = group.Value;
    //                        //var ac = Activator.CreateInstance(typeof(Reducer<>).MakeGenericType(subStateType),);
    //                        var m = subReducer.Method as System.Reflection.MethodInfo;
    //                        //var r = m?.Invoke(null, );
    //                        //var t = subReducer.Target as object;
    //                        var func = Delegate.CreateDelegate(typeof(Reducer<>).MakeGenericType(subStateType),m);
    //                        var newS = func.DynamicInvoke(subStateValue, action);

    //                        var subNewState = subReducer(subStateValue, action);
    //                        nextState.SetPropertyValue<T>(key, subNewState as object);
    //                    }
    //                }
    //            }

    //            return nextState;
    //        };
    //    }
    //}


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