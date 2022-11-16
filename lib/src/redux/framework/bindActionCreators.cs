using Redux.Basic;
using System.Reflection;
using Action = Redux.Basic.Action;
namespace Redux.Framework;

public static class ActionCreator
{
    public static Action<T1> bind<T1>(Func<T1, Action> func, Dispatch dispatch) => (t1) => dispatch(func(t1));
    public static Action<T1, T2> bind<T1, T2>(Func<T1, T2, Action> func, Dispatch dispatch) => (t1, t2) => dispatch(func(t1, t2));
    public static Action<T1, T2, T3> bind<T1, T2, T3>(Func<T1, T2, T3, Action> func, Dispatch dispatch) => (t1, t2, t3) => dispatch(func(t1, t2, t3));

    public static Func<T1, Action> bindAction<T1>(Func<T1, Action> func, Dispatch dispatch) => (t1) =>
    {
        var action = func(t1);
        dispatch(action);
        return action;
    };

    public static Func<T1, T2, Action> bindAction<T1, T2>(Func<T1, T2, Action> func, Dispatch dispatch) => (t1, t2) =>
    {
        var action = func(t1, t2);
        dispatch(action);
        return action;
    };

    public static Func<T1, T2, T3, Action> bindAction<T1, T2, T3>(Func<T1, T2, T3, Action> func, Dispatch dispatch) => (t1, t2, t3) =>
    {
        var action = func(t1, t2, t3);
        dispatch(action);
        return action;
    };

    public static System.Action bindAction(Action action, Dispatch dispatch) => () => dispatch(action);

    ////public static void Dispatch(this Action action, Dispatch dispatch) => dispatch(action);
}
