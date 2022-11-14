using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux;

public class Reducer
{
    ////public static Reducer<T> combineReducers<T>(Dictionary<string, dynamic> map)
    ////{
    ////    if (map == null || !map.Any())
    ////    {
    ////        return null;
    ////    }
    ////    else
    ////    {
    ////        return (T state, Action action) =>
    ////        {
    ////            T nextState = state;
    ////            foreach (var group in map)
    ////            {
    ////                string? key = group.Key as string;
    ////                if (!string.IsNullOrEmpty(key))
    ////                {

    ////                    Type entityType = typeof(T);
    ////                    PropertyInfo? subStateProperty = entityType.GetProperty(key);
    ////                    Type? subStateType = subStateProperty?.PropertyType;
    ////                    var subStateValue = subStateProperty?.GetValue(state);

    ////                    if(subStateProperty != null && subStateType != null)
    ////                    {
    ////                        var subReducer = group.Value;
    ////                        //var ac = Activator.CreateInstance(typeof(Reducer<>).MakeGenericType(subStateType),);
    ////                        var m = subReducer.Method as System.Reflection.MethodInfo;
    ////                        //var r = m?.Invoke(null, );
    ////                        //var t = subReducer.Target as object;
    ////                        var func = Delegate.CreateDelegate(typeof(Reducer<>).MakeGenericType(subStateType),m);
    ////                        var newS = func.DynamicInvoke(subStateValue, action);

    ////                        var subNewState = subReducer(subStateValue, action);
    ////                        nextState.SetPropertyValue<T>(key, subNewState as object);
    ////                    }
    ////                }
    ////            }

    ////            return nextState;
    ////        };
    ////    }
    ////}
}
