using Redux.Basic;
using Action = Redux.Basic.Action;
namespace Redux.Effect;

/// Interrupt if not null not false
/// bool for sync-functions, interrupted if true
/// Future<void> for async-functions, should always be interrupted.
public delegate dynamic Effect<T>(Action action, Context<T> ctx);

public delegate Task SubEffect<T>(Action action, Context<T> ctx);

////  Seen in effect-part
public abstract class Context<T>
{
    /// Get the latest state
    public T? state { get; }

    /// The way to send action, which will be consumed by self, or by broadcast-module and store.
    public abstract dynamic Dispatch(Action action);

    ///// Get BuildContext from the host-widget
    ////public BuildContext? context { get; }
}

public class StoreContext<T> : Context<T>
{
    private Store<T> _store;
    public StoreContext(Store<T> store)
    {
        _store = store;
    }

    public new T state => _store.GetState();

    public override dynamic Dispatch(Action action)
    {
        _store.Dispatch(action);
        return this;
    }
}

