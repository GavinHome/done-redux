namespace Composite;
internal class CompositeReducer
{
    internal static Redux.Reducer<CompositeState> buildReducer()
    {
        var map = new Dictionary<Object, Redux.Reducer<CompositeState>>();
        map.Add(CompositeAction.init, _init);
        return Redux.Converter.asReducers<CompositeState>(map);
    }

    private static CompositeState _init(CompositeState state, Redux.Action action)
    {
        CompositeState newState = CompositeState.initState();
        return newState;
    }
}
