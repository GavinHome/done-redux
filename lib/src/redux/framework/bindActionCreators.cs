namespace Redux;

public static class ActionCreator
{
    public static System.Action<T1> bind<T1>(Func<T1, Redux.Action> func, Dispatch dispatch) => (t1) => dispatch(func(t1));
    public static System.Action<T1, T2> bind<T1, T2>(Func<T1, T2, Redux.Action> func, Dispatch dispatch) => (t1, t2) => dispatch(func(t1, t2));
    public static System.Action<T1, T2, T3> bind<T1, T2, T3>(Func<T1, T2, T3, Redux.Action> func, Dispatch dispatch) => (t1, t2, t3) => dispatch(func(t1, t2, t3));

    public static Func<T1, Redux.Action> bindAction<T1>(Func<T1, Redux.Action> func, Dispatch dispatch) => (t1) =>
    {
        var action = func(t1);
        dispatch(action);
        return action;
    };

    public static Func<T1, T2, Redux.Action> bindAction<T1, T2>(Func<T1, T2, Redux.Action> func, Dispatch dispatch) => (t1, t2) =>
    {
        var action = func(t1, t2);
        dispatch(action);
        return action;
    };

    public static Func<T1, T2, T3, Redux.Action> bindAction<T1, T2, T3>(Func<T1, T2, T3, Redux.Action> func, Dispatch dispatch) => (t1, t2, t3) =>
    {
        var action = func(t1, t2, t3);
        dispatch(action);
        return action;
    };

    public static System.Action bindAction(Redux.Action action, Dispatch dispatch) => () => dispatch(action);
}
