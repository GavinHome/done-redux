using System;
using Redux.Dependencies;

namespace Redux.Maui.Context;

/////  Seen in effect-part
//abstract class Context<T>
//{
//    /// Get the latest state
//    public virtual T state { get; set; }

//    /// Get BuildContext from the host-widget
//    public virtual BuildContext context { get; set; }

//    public virtual Store<T> store { get; set; }

//    public virtual Enhancer<T> enhancer { get; set; }

//    /// The way to send action, which will be consumed by self, or by broadcast-module and store.
//    public abstract void dispatch(Action action);

//    /// The way to build slot component which is configured in Dependencies.slots
//    /// such as custom mask or dialog
//    public abstract Widget buildComponent(String name, Widget defaultWidget);
//}

///// LogicContext
//class LogicContext<T> : Context<T>
//{
//    AbstractLogic<T> logic;
//    private BuildContext _buildContext { get; set; }
//    private Get<T> getState;
//    private Dispatch _dispatch;
//    Dispatch _effectDispatch;

//    public LogicContext(
//        Logic<T> logic,
//        Store<T> store,
//        BuildContext buildContext,
//        Get<T> getState,
//        Enhancer<T> enhancer
//    )
//    {
//        this.logic = logic;
//        this.store = store;
//        this._buildContext = buildContext;
//        this.getState = getState;
//        this.enhancer = enhancer;

//        /// create Dispatch
//        //  _dispatch = logic.createDispatch(
//        //    _effectDispatch,
//        //         logic.createNextDispatch(
//        //  this,
//        //  enhancer,
//        //),
//        //this,
//        //  );
//    }

//    public override Store<T> store { get; set; }
//    public override Enhancer<T> enhancer { get; set; }
//    public override BuildContext context => _buildContext;
//    public override T state => getState();
//    public override void dispatch(Action action) => _dispatch(action);

//    public override Widget buildComponent(String name, Widget defaultWidget)
//    {
//        //Dependent<T> dependent = logic.slot(name);
//        //Widget result = dependent?.buildComponent(store, getState, enhancer: enhancer);

//        //return result ?? (defaultWidget ?? Container());
//        return null;
//    }
//}

//class ComponentContext<T> : LogicContext<T>
//{
//    ViewBuilder<T> view;

//    Widget _widgetCache;
//    T _latestState;

//    public ComponentContext(
//        AbstractComponent<T> logic,
//        Store<T> store,
//        BuildContext buildContext,
//        Get<T> getState,
//        ViewBuilder<T> view,
//        Enhancer<T> enhancer
//   ) : base(logic: logic, store: store, buildContext: buildContext, getState: getState, enhancer: enhancer)
//    {
//        this.view = view;
//        this._latestState = state;
//    }


//    public Widget buildWidget()
//    {
//        Widget result = _widgetCache;
//        if (result == null)
//        {
//            result = _widgetCache = view(state, dispatch);
//            //dispatch(LifecycleCreator.build(name));
//        }

//        return result;
//    }
//}

