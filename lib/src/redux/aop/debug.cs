namespace Redux;

public static class Aop
{
    public static bool isTest { get; private set; }

    /// Is app run a debug mode.
    public static bool isDebug()
    {
        return isTest || System.Diagnostics.Debugger.IsAttached;
    }

    public static void setTest()
    {
        Aop.isTest = true;
    }
}
