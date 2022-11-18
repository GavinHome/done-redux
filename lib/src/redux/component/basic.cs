using Redux;
using Redux.Dependencies;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata.Ecma335;
using static System.Formats.Asn1.AsnWriter;

namespace Redux;

/// Component's view part
/// 1.State is used to decide how to render
/// 2.Dispatch is used to send actions
/// 3.ViewService is used to build sub-components or adapter.
public delegate dynamic ViewBuilder<T>(
      T state,
      Dispatch dispatch
    //ViewService viewService
    );

public interface AbstractComponent<T>
{
    /// How to build component instance
}

public abstract class Component<T> : Logic<T>, AbstractComponent<T>
{
    private ViewBuilder<T> _view;
    protected WidgetWrapper protectedWrapper => _wrapperByDefault;

    public Component(Reducer<T> reducer, Dependencies<T> dependencies) : base(reducer, dependencies)
    {
    }

    public Component(Reducer<T> reducer) : base(reducer, null)
    {
    }

    protected Component(ViewBuilder<T> view, Reducer<T> reducer, Dependencies<T> dependencies) : base(reducer, dependencies)
    {
        this._view = view;
    }

    protected dynamic _wrapperByDefault(dynamic child) => child;

    public dynamic buildComponent(Store<Object> store, Get<Object> getter)
    {
        return protectedWrapper(new ComponentWidget<T>(component: this, store: store));
    }
}

class ComponentWidget<T>//: StatefulWidget
{
    Component<T> component;
    Store<Object> store;

    public ComponentWidget(Component<T> component, Store<Object> store)
    {
        this.component = component;
        this.store = store;
    }
}

/// Wrapper ComponentWidget if needed like KeepAlive, RepaintBoundary etc.
public delegate dynamic WidgetWrapper(dynamic child);

/// init store's state by route-params
public delegate T InitState<T, P>(P param);

public abstract class Page<T, P> : Component<T>
{
    private InitState<T, P> _initState;

    protected Page(InitState<T, P> initState, ViewBuilder<T> view, Reducer<T> reducer, Dependencies<T> dependencies) : base(view, reducer, dependencies)
    {
        _initState = initState;
    }

    public dynamic buildPage(P param) => protectedWrapper(new _PageWidget<T, P>(page: this, param: param));
}

class _PageWidget<T, P>//: StatefulWidget
{
    //  final Page<T, P> page;
    //  final P param;

    public _PageWidget(Page<T, P> page, dynamic param)
    { }
}

