namespace Composite;
internal enum CompositeAction
{
    init,
}

internal class CompositeActionCreator
{
    internal static Redux.Action init(int payload)
    {
        return new Redux.Action(CompositeAction.init, payload);
    }
}
