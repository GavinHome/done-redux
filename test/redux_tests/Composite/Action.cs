using System;

namespace Composite;
using Action = Redux.Basic.Action;

internal enum CompositeAction
{
    init,
}

internal class CompositeActionCreator
{
    internal static Action init(int payload)
    {
        return new Action(CompositeAction.init, payload);
    }
}
