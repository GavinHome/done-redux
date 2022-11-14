using Redux.Basic;
using Redux.Connector;
using Redux.Dependencies.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.Dependencies;

internal static class Creator
{
    public static Dependent<K>  createDependent<K, T>(AbstractConnector<K, T> connector, AbstractLogic<T> logic) =>
        logic != null ? new _Dependent<K, T>(connector: connector, logic: logic) : null;
}
