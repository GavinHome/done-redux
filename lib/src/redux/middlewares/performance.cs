using System.Diagnostics;
using Redux.Basic;
using Action = Redux.Basic.Action;

namespace Redux;

/// Middleware for print action dispatch performance by time consuming.
/// It works on debug mode.
public partial class Middlewares
{
    public static Middleware<T> performanceMiddleware<T>(String tag = "done-redux")
    {
        return (Dispatch Dispatch, Get<T> getState) =>
            (Dispatch next) =>
            {
                System.Action<Object> print = (Object obj) => Console.WriteLine(obj);
                Dispatch performance = (Action action) =>
                {
                    DateTime markPrev = DateTime.Now;
                    next(action);
                    DateTime markNext = DateTime.Now;
                    print($"[{tag}] performance: {action.Type} {(markNext - markPrev).TotalMilliseconds} millisecond");
                };

                return Aop.isDebug() ? performance : next;
            };
    }
}
