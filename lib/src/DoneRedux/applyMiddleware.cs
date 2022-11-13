using System;
using System.Linq;
using System.Xml.Linq;
using Redux.Framework;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Redux.Framework;

public static class Middleware
{
    ///// Accumulate a list of Middleware that enhances Dispatch to the Store.
    ///// The wrapped direction of the Store.dispatch is from inside to outside.

    public static StoreEnhancer<T> applyMiddleware<T>(params Middleware<T>[] middlewares)
    {
        StoreEnhancer<T> inner = (StoreCreator<T> creator) => (T initState, Reducer<T> reducer) =>
        {
            Store<T> store = creator(initState, reducer);
            Dispatch initialValue = store.dispatch;
            store.dispatch = (Redux.Framework.Action action) =>
            {
                throw new Exception("Dispatching while constructing your middleware is not allowed.  " +
                    "Other middleware would not be applied to this dispatch.");
            };
            store.dispatch = middlewares.Select(middleware => middleware(
                dispatch: (Redux.Framework.Action action) => store.dispatch(action),
                getState: store.getState
            ))
            .Aggregate(initialValue, (Dispatch previousValue, Composable<Dispatch> element) => element(previousValue));
            return store;
        };

        return middlewares == null || !middlewares.Any()? null : inner;
    }
}

