using Redux.Basic;
using Redux.Effect;
using Action = Redux.Basic.Action;

namespace Redux;

public static partial class Helper
{
    readonly static object _SUB_EFFECT_RETURN_NULL = new object();

    public static Effect<T>? combineEffects<T>(Dictionary<object, SubEffect<T>> map) => map == null || !map.Any()
        ? null : (Action action, Context<T> ctx) =>
        {
            SubEffect<T> subEffect = map.FirstOrDefault(entry => action.Type.Equals(entry.Key)).Value;
            if (subEffect != null)
            {
                return subEffect.Invoke(action, ctx) ?? _SUB_EFFECT_RETURN_NULL;
            }

            ////kip-lifecycle-actions
            //if (action.Type is Lifecycle)
            //{
            //    return _SUB_EFFECT_RETURN_NULL;
            //}

            ////no subEffect
            return null;
        };
}
