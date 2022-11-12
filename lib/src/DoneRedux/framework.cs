namespace Redux.Framework;

public class Store<T>
{
    private T state;
    private IList<Action> listeners;

    public Store(T initState)
    {
        this.state = initState;
        this.listeners = new List<System.Action>();
    }

    public async Task subscribe(Action listener)
    {
        await Task.Run(() =>
        {
            listeners.Add(listener);
        });
    }

    public async Task changeState(T newState)
    {
        await Task.Run(() =>
        {
            this.state = newState;

            foreach (var listener in listeners)
            {
                listener();
            }
        });
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
    public static Store<T> createStore(T initState)
    {
        return new Store<T>(initState);
    }

    /////// Definition of a standard subscription function.
    /////// input a subscriber and output an anti-subscription function.
    ///public delegate void Subscribe(System.Action callback);
}
