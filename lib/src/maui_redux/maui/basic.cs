namespace Redux.Maui;

public abstract class Widget : View
{
}
public abstract class BuildContext
{
}

/// Wrapper ComponentWidget if needed like KeepAlive, RepaintBoundary etc.
public delegate Widget WidgetWrapper(Widget child);

/// init store's state by route-params
public delegate T InitState<T, P>(P param);

public abstract class Component<T> : AbstractComponent<T>
{
    private ViewBuilder<T> _view;
    protected WidgetWrapper protectedWrapper => _wrapperByDefault;

    protected Component(ViewBuilder<T> view, Reducer<T> reducer, Dependencies<T> dependencies) : base(reducer, dependencies)
    {
        this._view = view;
    }

    protected Widget _wrapperByDefault(Widget child) => child;

    public Widget buildComponent(Store<Object> store, Get<Object> getter)
    {
        return protectedWrapper(new ComponentWidget<T>(component: this, store: store));
    }
}

class ComponentWidget<T>: Widget
{
    AbstractComponent<T> component;
    Store<Object> store;

    public ComponentWidget(AbstractComponent<T> component, Store<Object> store)
    {
        this.component = component;
        this.store = store;
    }
}

public abstract class Page<T, P> : Component<T>
{
    private InitState<T, P> _initState;

    protected Page(InitState<T, P> initState, ViewBuilder<T> view, Reducer<T> reducer, Dependencies<T> dependencies) : base(view, reducer, dependencies)
    {
        _initState = initState;
    }

    public Widget buildPage(P param) => protectedWrapper(new _PageWidget<T, P>(page: this, param: param));
}

class _PageWidget<T, P> : Widget
{
    Page<T, P> page;
    P param;

    public _PageWidget(Page<T, P> page, dynamic param)
    {
        this.page = page;
        this.param = param;
    }
}

