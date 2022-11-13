using System;
using Redux.Framework;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using static System.Collections.Specialized.BitVector32;

namespace Redux;

/// Middleware for print action dispatch performance by time consuming.
/// It works on debug mode.
public partial class Middleware
{
    public static Middleware<T> performanceMiddleware<T>(bool isDebug, String tag = "done-redux")
    {
        return (Dispatch Dispatch, Get<T> getState) =>
            (Dispatch next) =>
            {
                System.Action<Object> print = (Object obj) => Console.WriteLine(obj);
                Dispatch performance = (Redux.Framework.Action action) =>
                {
                    int markPrev = DateTime.Now.Microsecond;
                    next(action);
                    int markNext = DateTime.Now.Microsecond;
                    print($"[{tag}] performance: {action.Type} {markNext - markPrev}");
                };

                return isDebug ? performance : next;
            };
    }
}
