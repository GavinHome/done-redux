using Counter;
using Redux.Basic;
using Redux.Utils;

namespace Composite;
using Action = Redux.Basic.Action;

internal class CompositeReducer
{
    internal static Reducer<CompositeState> buildReducer()
    {
        var map = new Dictionary<Object, Reducer<CompositeState>>();
        map.Add(CompositeAction.init, _init);
        return Redux.Helper.asReducers<CompositeState>(map);
    }

    private static CompositeState _init(CompositeState state, Action action)
    {
        CompositeState newState = CompositeState.initState();
        return newState;
    }
}
