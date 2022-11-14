using Redux.Basic;
using Action = Redux.Basic.Action;

namespace Redux;

/// Middleware for print action dispatch.
/// It works on debug mode.
public partial class Middlewares
{
    public static Middleware<T> logMiddleware<T>(System.Func<T,String> monitor, String tag = "done-redux")
	{
		return (Dispatch Dispatch, Get<T> getState) =>
			(Dispatch next) =>
			{
                System.Action<Object> print = (Object obj) => Console.WriteLine(obj);

                Dispatch log = (Action action) =>
                {
                    print($"---------- [{tag}] ----------");
                    print($"[{tag}] {action.Type} {action.Payload}");

                    T prevState = getState();
                    if (monitor != null)
                    {
                        print($"[{tag}] prev-state: {monitor(prevState)}");
                    }

                    next(action);

                    T nextState = getState();
                    if (monitor != null)
                    {
                        print($"[{tag}] next-state: {monitor(nextState)}");
                    }

                    ////if (prevState == nextState)
                    ////{
                    ////    print($"[{tag}] warning: {action.type} has not been used.");
                    ////}

                    print($"========== [{tag}] ================");
                };

			    return Aop.isDebug() ? log : next;
			};
	}
}

