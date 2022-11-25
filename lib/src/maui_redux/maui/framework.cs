using Redux.Adapter;

namespace Redux.Maui;

/// Wrapper ComponentWidget if needed like KeepAlive, RepaintBoundary etc.
public delegate Widget WidgetWrapper(Widget child);

/// init store's state by route-params
public delegate T InitState<T, P>(P param);

/// store updater
public delegate Store<T> StoreUpdater<T>(Store<T> store);

/// Adapter's view part
/// 1.State is used to decide how to render
/// 2.Dispatch is used to send actions
/// 3.ViewService is used to build sub-components or adapter.
public delegate dynamic AdapterBuilder<T>(
  T state,
  Dispatch dispatch
// ViewService viewService,
);

/// AOP on adapter
public delegate Composable<AdapterBuilder<dynamic>> AdapterMiddleware<T>(
  AbstractAdapter<dynamic> adapter,
  Store<T> store
);

/// AOP in page on store, view, adapter, effect...
public abstract class Enhancer<T>
{
    public abstract StoreCreator<T> storeEnhance(StoreCreator<T> creator);
}

/// Enhancer Default
class EnhancerDefault<T> : Enhancer<T>
{
    StoreEnhancer<T> _storeEnhancer;
    AdapterMiddleware<T> _adapterEnhancer;

    public override StoreCreator<T> storeEnhance(StoreCreator<T> creator) => _storeEnhancer?.Invoke(creator) ?? creator;
}

public abstract class Component<T> : AbstractComponent<T>
{
    private ViewBuilder<T> _view;
    protected WidgetWrapper protectedWrapper => _wrapperByDefault;
    public ViewBuilder<T> protectedView => _view;

    protected Component(ViewBuilder<T> view, Reducer<T> reducer, Dependencies<T> dependencies) : base(reducer, dependencies)
    {
        this._view = view;
    }

    protected Widget _wrapperByDefault(Widget child) => child;

    public Widget buildComponent(Store<T> store, Get<T> getter, Enhancer<T> enhancer)
    {
        return protectedWrapper(new ComponentWidget<T>(component: this, getter: getter, store: store, enhancer: enhancer));
    }
}

public class ComponentWidget<T> : StatefulWidget
{
    Component<T> component;
    Get<T> getter;
    Store<T> store;
    Enhancer<T> enhancer;

    public ComponentWidget(Component<T> component, Get<T> getter, Store<T> store, Enhancer<T> enhancer)
    {
        this.component = component;
        this.getter = getter;
        this.store = store;
        this.enhancer = enhancer;
    }

    public override Widget buildWidget()
    {
        return this.component.protectedView(this.store.GetState(), this.store.Dispatch);
    }
}

public abstract class Page<T, P> : Component<T>
{
    private InitState<T, P> _initState;

    public Enhancer<T> enhancer { get; private set; }

    /// connect with other stores
    private IList<StoreUpdater<T>> _storeUpdaters = new List<StoreUpdater<T>>();

    protected Page(InitState<T, P> initState, ViewBuilder<T> view, Reducer<T> reducer, Dependencies<T> dependencies) : base(view, reducer, dependencies)
    {
        _initState = initState;

        ////TODO: to finish enhancer
        enhancer = new EnhancerDefault<T>(
        //middleware: middleware,
        //viewMiddleware: viewMiddleware,
        //effectMiddleware: effectMiddleware,
        //adapterMiddleware: adapterMiddleware,
        );
    }

    public Widget buildPage(P param) => protectedWrapper(new _PageWidget<T, P>(page: this, param: param));

    public Store<T> createStore(P param) => updateStore(StoreCreator.createStore<T>(
      _initState(param),
      createReducer(),
      enhancer: enhancer.storeEnhance
    ));

    public Store<T> updateStore(Store<T> store) =>
        _storeUpdaters.Aggregate(store, (Store<T> previousValue, StoreUpdater<T> element) => element(previousValue));
}

class _PageWidget<T, P> : StatefulWidget
{
    public Page<T, P> page { get; private set; }
    public P param { get; private set; }

    public _PageWidget(Page<T, P> page, dynamic param)
    {
        this.page = page;
        this.param = param;
    }

    public override Widget buildWidget()
    {
        var component = new _PageState<T, P>(this).build(null) as ComponentWidget<T>;
        return component.buildWidget();
    }
}

class _PageState<T, P> : State<_PageWidget<T, P>>
{
    private Store<T> _store;

    private new _PageWidget<T, P> widget;

    public _PageState(_PageWidget<T, P> widget)
    {
        this.widget = widget;
        this.initState();
    }

    protected override void initState()
    {
        base.initState();
        _store = widget.page.createStore(widget.param);
    }

    public override Widget build(BuildContext context)
    {
        return widget.page.buildComponent(
          _store,
          _store.GetState,
          enhancer: widget.page.enhancer
        );
    }
}


public abstract class ViewModelUpdater<T>: Updater
{
    private Store<T> _store;
    public Store<T> Store => _store;
    private T _state;

    public ViewModelUpdater()
    {
        _store = StoreCreator.createStore<T>(initState(), reducer(), Enhancer, Dependencies);

        _store.Subscribe(() =>
        {
            State = _store.GetState();
            NotifyChange();
        });
    }

    protected T State { get => _state; private set => SetState(ref _state, value); }

    protected Get<T> GetState => _store.GetState;
    protected Dispatch Dispatch => _store.Dispatch;
    protected abstract T initState();
    protected abstract Reducer<T> reducer();
    protected virtual Dependencies<T> Dependencies { get; } = null;
    protected virtual StoreEnhancer<T> Enhancer { get; set; } = null;
    protected abstract void NotifyChange();
}