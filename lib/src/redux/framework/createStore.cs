using Redux.Basic;
using Redux.Dependencies.Basic;

namespace Redux;

public partial class Creator
{
    /// <summary>
    /// Create Store
    /// </summary>
    /// <typeparam name="T">The type of state.</typeparam>
    /// <param name="initState">The state object instance.</param>
    /// <returns>The store object</returns>
    /// <exception cref="NotImplementedException"></exception>
    public static Store<T> createStore<T>(T initState, Reducer<T> reducer)
    {
        Store<T> store = new Store<T>(initState, reducer);
        return store;
    }

    /// create a store with enhancer
    public static Store<T> createStore<T>(T preloadedState, Reducer<T> reducer, StoreEnhancer<T> enhancer)
    {
        return enhancer != null ? enhancer(createStore)(preloadedState, reducer) : createStore(preloadedState, reducer);
    }

    public static Store<T> createStore<T>(T preloadedState, Reducer<T> reducer, StoreEnhancer<T> enhancer, Dependencies<T> dependencies)
    {
        var combineReducers = Reducer.combineReducers(new List<Reducer<T>>() { reducer, dependencies.createReducer() });
        return enhancer != null ? enhancer(createStore)(preloadedState, combineReducers) : createStore(preloadedState, reducer);
    }
}
