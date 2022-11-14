using Redux.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.Framework;

public static class bindCreator
{
    public static System.Action<T1> bindActionCreator<T1>(Func<T1, Redux.Basic.Action> func, Dispatch dispatch)
    {
        return (T1 t1) => dispatch(func(t1));
    }

    public static System.Action<T1, T2> bindActionCreator<T1, T2>(Func<T1, T2, Redux.Basic.Action> func, Dispatch dispatch)
    {
        return (T1 t1, T2 t2) => dispatch(func(t1, t2));
    }

    public static System.Action<T1, T2, T3> bindActionCreator<T1, T2, T3>(Func<T1, T2, T3, Redux.Basic.Action> func, Dispatch dispatch)
    {
        return (T1 t1, T2 t2, T3 t3) => dispatch(func(t1, t2, t3));
    }

    public static void Dispatch(this Redux.Basic.Action action, Dispatch dispatch) => dispatch(action);

    ////public static T bindActionCreators<T>(T instance, Dispatch dispatch)
    ////{
    ////    Type type = typeof(T);
    ////    if (instance == null || typeof(T) != typeof(Object))
    ////    {
    ////        throw new ArgumentException("The instance is null. Or The type is error.");
    ////    }

    ////    var keys = type.GetMembers(System.Reflection.BindingFlags.Public).Where(m => m.MemberType == System.Reflection.MemberTypes.Method).ToList();
    ////    foreach (var key in keys)
    ////    {
    ////        if (key.GetType() == typeof(Redux.Basic.Action))
    ////        {

    ////        }
    ////    }

    ////    return instance;
    ////}
}
