using System.Reflection;
namespace DoneRedux.Utils;

public static class Helper
{
    /// <summary>
    /// Get property value by property name
    /// </summary>
    /// <param name="entity">The entity instance.</param>
    /// <param name="name">The name of property.</param>
    /// <returns>property value</returns>
    public static object? GetPropertyValue<T>(this T entity, string name)
    {
        if(entity == null)
        {
            throw new ArgumentNullException("The entity is null.");
        }

        Type entityType = typeof(T);
        PropertyInfo? proInfo = entityType.GetProperty(name);
        object? result = proInfo?.GetValue(entity);
        return result;

    }

    /// <summary>
    /// Set property value by property name
    /// </summary>
    /// <param name="entity">The entity instance.</param>
    /// <param name="propertyName">The name of property.</param>
    /// <param name="propertyValue">The value of property.</param>
    /// <returns>property value</returns>
    public static object? SetPropertyValue<T>(this T entity, string name, object value)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("The entity is null.");
        }

        Type entityType = typeof(T);
        PropertyInfo? proInfo = entityType.GetProperty(name);
        proInfo?.SetValue(entity, value);
        return entity;
    }

    //public static Delegate ConvertDelegate(this Delegate src, Type targetType, bool doTypeCheck)
    //{
    //    //Is it null or of the same type as the target?
    //    if (src == null || src.GetType() == targetType)
    //        return src;
    //    //Is it multiple cast?
    //    return src.GetInvocationList().Count() == 1
    //        ? Delegate.CreateDelegate(targetType, src.Target, src.Method, doTypeCheck)
    //        : src.GetInvocationList().Aggregate<Delegate, Delegate>
    //            (null, (current, d) => Delegate.Combine(current, ConvertDelegate(d, targetType, doTypeCheck)));
    //}

    //public static Reducer<object> Convert<T>(this Reducer<T> src)
    //{
    //    var targetType = typeof(Reducer<object>);
    //    return src.ConvertDelegate(targetType, true) as Reducer<object>;
    //}
}