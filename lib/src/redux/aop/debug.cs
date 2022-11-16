using System.Diagnostics;

namespace Redux;

public static class Aop
{
    public static bool isTest { get; private set; }

    /// Is app run a debug mode.
    public static bool isDebug()
    {
        return isTest || Debugger.IsAttached;
    }

    public static void setTest()
    {
        Aop.isTest = true;
    }
}
