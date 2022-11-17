namespace Redux;

public class Enhancers
{
    ///// Accumulate a list of Middleware that enhances Dispatch to the Store.
    ///// The wrapped direction of the Store.dispatch is from inside to outside.

    public static StoreEnhancer<T>? applyMiddleware<T>(params Middleware<T>[] middlewares)
    {
        StoreEnhancer<T> inner = (StoreCreator<T> creator) => (T initState, Reducer<T> reducer) =>
        {
            Store<T> store = creator(initState, reducer);
            Dispatch initialValue = store.Dispatch;
            store.Dispatch = (Action action) =>
            {
                throw new Exception("Dispatching while constructing your middleware is not allowed. " +
                    "Other middleware would not be applied to this dispatch.");
            };
            store.Dispatch = middlewares.Select(middleware => middleware(
                dispatch: (Action action) => store.Dispatch(action),
                getState: store.GetState
            ))
            .Aggregate(initialValue, (Dispatch previousValue, Composable<Dispatch> element) => element(previousValue));
            return store;
        };

        return (middlewares == null || !middlewares.Any()) ? null : inner;
    }
}

