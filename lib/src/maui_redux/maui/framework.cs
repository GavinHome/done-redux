﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using Java.Lang;
using Redux.Adapter;
using Redux.Dependencies;

namespace Redux.Maui;

public abstract class Widget : Microsoft.Maui.Controls.View
{
    public abstract Element createElement();
}

public abstract class BuildContext
{
    protected abstract Widget widget { get; }
}

public class Element: BuildContext
{
    protected override Widget widget => _widget!;
    protected Widget _widget;
}

public abstract class StatefulWidget: Widget {
    public override Element createElement() => new StatefulElement(this);
    public abstract State<StatefulWidget> createState();
}

///// An [Element] that uses a [StatefulWidget] as its configuration.
class StatefulElement : ComponentElement
{
    /// Creates an element that uses the given widget as its configuration.
    public StatefulElement(StatefulWidget widget) : base(widget)
    {
        //_state = widget.createState();
        //state._element = this;
        //state._widget = widget;
    }

    State<StatefulWidget> state => _state!;
    State<StatefulWidget>? _state;

    protected override Widget build() => state.build(this);
}

abstract class ComponentElement : Element
{
    /// Creates an element that uses the given widget as its configuration.
    public ComponentElement(Widget widget) {
        this._widget = widget;
    }

    /// Subclasses should override this function to actually call the appropriate
    /// `build` function (e.g., [StatelessWidget.build] or [State.build]) for
    /// their widget.
    protected abstract Widget build();
}



public abstract class State<T> where T : StatefulWidget
{
    public T widget => _widget!;
#pragma warning disable CS8632 // 只能在 "#nullable" 注释上下文内的代码中使用可为 null 的引用类型的注释。
    T? _widget;
#pragma warning restore CS8632 // 只能在 "#nullable" 注释上下文内的代码中使用可为 null 的引用类型的注释。

    protected virtual void initState()
    {
    }

    public abstract Widget build(BuildContext context);
}

/// Wrapper ComponentWidget if needed like KeepAlive, RepaintBoundary etc.
public delegate Widget WidgetWrapper(Widget child);

/// init store's state by route-params
public delegate T InitState<T, P>(P param);

public delegate Store<T> StoreUpdater<T>(Store<T> store);

public abstract class Component<T> : AbstractComponent<T>
{
    private ViewBuilder<T> _view;
    protected WidgetWrapper protectedWrapper => _wrapperByDefault;

    protected Component(ViewBuilder<T> view, Reducer<T> reducer, Dependencies<T> dependencies) : base(reducer, dependencies)
    {
        this._view = view;
    }

    protected Widget _wrapperByDefault(Widget child) => child;

    public Widget buildComponent(Store<T> store, Get<T> getter, Enhancer<T> enhancer)
    {
        return protectedWrapper(new ComponentWidget<T>(component: this, getter: getter, store: store, enhancer: enhancer));
    }


    public ComponentState<T> createState() => new ComponentState<T>();
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

    public override State<StatefulWidget> createState() => component.createState() as State<StatefulWidget>;
}

public class ComponentState<T> : State<ComponentWidget<T>>
{
    public override Widget build(BuildContext context)
    {
        throw new NotImplementedException();
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


    public override State<StatefulWidget> createState() => new _PageState<T, P>() as State<StatefulWidget>;
}

class _PageState<T, P> : State<_PageWidget<T, P>> 
{
    private Store<T> _store;

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


class EnhancerDefault<T>: Enhancer<T> {
    StoreEnhancer<T> _storeEnhancer;
    AdapterMiddleware<T> _adapterEnhancer;

    public override StoreCreator<T> storeEnhance(StoreCreator<T> creator)
    {
        throw new NotImplementedException();
    }
}

//public static class PageStore<T, P>
//{

//    public Store<T> createPageStore<T,P>(InitState<T, P> initState, Reducer<T> reducer, Dependencies<T> dependencies)
//    {
//        return null;
//    }
//}