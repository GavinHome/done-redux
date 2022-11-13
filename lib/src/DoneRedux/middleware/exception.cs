using System;
using Redux.Framework;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using System.Diagnostics;

namespace Redux;

/// Middleware for print action dispatch.
/// It works on debug mode.
public partial class Middleware
{
    public static Middleware<T> exceptionMiddleware<T>(String tag = "done-redux")
    {
        return (Dispatch Dispatch, Get<T> getState) =>
            (Dispatch next) =>
            {
                System.Action<Object> print = (Object obj) => Console.WriteLine(obj);
                Dispatch exception = (Redux.Framework.Action action) =>
                {
                    System.Action<Object> print = (Object obj) => Console.WriteLine(obj);

                    

                    try
                    {
                        next(action);
                    }
                    catch (Exception ex)
                    {
                        print($"[{tag}] {action.Type} error:  {ex}");
                    }
                };

                return next;
            };
    }
}
