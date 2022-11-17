using Redux.Dependencies;

namespace Redux;

internal static class DependentCreator
{
    public static Dependent<K>  createDependent<K, T>(AbstractConnector<K, T> connector, AbstractLogic<T> logic) =>
        logic != null ? new _Dependent<K, T>(connector: connector, logic: logic) : null;
}
