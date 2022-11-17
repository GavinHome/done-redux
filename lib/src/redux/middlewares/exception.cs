namespace Redux;

/// Middleware for print action dispatch.
/// It works on debug mode.
public partial class Middlewares
{
    public static Middleware<T> exceptionMiddleware<T>(String tag = "done-redux")
    {
        return (Dispatch Dispatch, Get<T> getState) =>
            (Dispatch next) =>
            {
                System.Action<Object> print = (Object obj) => Console.WriteLine(obj);
                Dispatch exception = (Action action) =>
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
