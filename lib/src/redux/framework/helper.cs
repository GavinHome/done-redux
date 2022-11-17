namespace Redux;

public static class Converter
{
    public static Reducer<T> asReducers<T>(Dictionary<object, Reducer<T>> map)
    {
        if (map == null || !map.Any())
        {
            return null;
        }
        else
        {
            return (state, action) =>
            {
                var fn = map.FirstOrDefault(entry => action.Type.Equals(entry.Key)).Value;
                if (fn != null) { return fn(state, action); }
                else return state;
            };
        }
    }
}
