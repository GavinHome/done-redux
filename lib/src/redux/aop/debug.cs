using System.Diagnostics;

namespace Redux;

public static class Aop
{
    /// Is app run a debug mode.
    public static bool isDebug()
    {
        return Debugger.IsAttached;
    }
}
