using Redux.Basic;
using System.Reflection;
using Action = Redux.Basic.Action;
namespace Redux.Framework;

public static class ActionCreator
{
    public static System.Action<T1> bind<T1>(Func<T1, Action> func, Dispatch dispatch) => (T1 t1) => dispatch(func(t1));
    public static System.Action<T1, T2> bind<T1, T2>(Func<T1, T2, Action> func, Dispatch dispatch) => (T1 t1, T2 t2) => dispatch(func(t1, t2));
    public static System.Action<T1, T2, T3> bind<T1, T2, T3>(Func<T1, T2, T3, Action> func, Dispatch dispatch) => (T1 t1, T2 t2, T3 t3) => dispatch(func(t1, t2, t3));

    public static System.Func<T1, Action> bindAction<T1>(Func<T1, Action> func, Dispatch dispatch) => (T1 t1) =>
    {
        var action = func(t1);
        dispatch(action);
        return action;
    };

    public static System.Func<T1, T2, Action> bindAction<T1, T2>(Func<T1, T2, Action> func, Dispatch dispatch) => (T1 t1, T2 t2) =>
    {
        var action = func(t1, t2);
        dispatch(action);
        return action;
    };

    public static System.Func<T1, T2, T3, Action> bindAction<T1, T2, T3>(Func<T1, T2, T3, Action> func, Dispatch dispatch) => (T1 t1, T2 t2, T3 t3) =>
    {
        var action = func(t1, t2, t3);
        dispatch(action);
        return action;
    };

    public static System.Action bindAction(Action action, Dispatch dispatch) => () => dispatch(action);

    public static void Dispatch(this Redux.Basic.Action action, Dispatch dispatch) => dispatch(action);

    ////public static object bindActionCreators(Type type, Dispatch dispatch, params object[] param)
    ////{
    ////    if (type == null)
    ////    {
    ////        throw new ArgumentException("The instance is null. Or The type is error.");
    ////    }
    ////    var map = new Dictionary<Type, object>();
    ////    var keys = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static| BindingFlags.Instance).Where(m => m.ReturnType == typeof(Action)).ToList();
    ////    foreach (var key in keys)
    ////    {
    ////        var paramsArry = key.GetParameters();
    ////        //var action = "";
    ////        //var dispatch = "";
    ////        //map.Add(key.Name, dispatch);
    ////    }
    ////    return map;
    ////}
}
