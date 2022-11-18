using Redux;
using Redux.Dependencies;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata.Ecma335;
using static System.Formats.Asn1.AsnWriter;

namespace Redux;

/// Component's view part
/// 1.State is used to decide how to render
/// 2.Dispatch is used to send actions
/// 3.ViewService is used to build sub-components or adapter.
public delegate dynamic ViewBuilder<T>(T state, Dispatch dispatch); //ViewService viewService

public abstract class AbstractComponent<T> : Logic<T>
{
    public AbstractComponent(Reducer<T> reducer, Dependencies<T> dependencies) : base(reducer, dependencies)
    {
    }

    public AbstractComponent(Reducer<T> reducer) : base(reducer, null)
    {
    }
}