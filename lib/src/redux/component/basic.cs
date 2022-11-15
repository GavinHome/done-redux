using Redux.Basic;
using Redux.Dependencies;
using Redux.Dependencies.Basic;

namespace Redux.Component;

public interface AbstractComponent<T>
{
    /// How to build component instance
}

public abstract class Component<T> : Logic<T>, AbstractComponent<T>
{
    public Component(Reducer<T> reducer, Dependencies<T> dependencies) : base(reducer, dependencies)
    {
    }

    public Component(Reducer<T> reducer) : base(reducer, null)
    {
    }
}

