using Redux.Basic;
using Redux.Connector;
using Redux.Dependencies.Basic;

namespace Redux.Dependencies;

public class _Dependent<T, P> : Dependent<T>
{
    AbstractConnector<T, P> connector;
    AbstractLogic<P> logic;
    SubReducer<T>? subReducer;

    public _Dependent(AbstractLogic<P> logic, AbstractConnector<T, P> connector)
    {
        this.connector = connector;
        this.logic = logic;
        subReducer = logic.createReducer != null ? connector.subReducer(logic.createReducer()) : null;
    }

    public override SubReducer<T> createSubReducer() => subReducer;

    public override Get<object> subGetter(Get<T> getter) => () => connector.Get(getter());
}

