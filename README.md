<h1>Done Redux</h1>

[![Build Status](https://travis-ci.org/gavinhome/done-redux.svg?branch=master)](https://travis-ci.org/gavinhome/done-redux) [![codecov](https://codecov.io/gh/gavinhome/done-redux/branch/master/graph/badge.svg)](https://codecov.io/gh/gvinhome/done-redux)



## What is Done Redux?

Done Redux is a combined state management framework based on Redux and .Net7, namely Donet Redux.
It is suitable for building .NET applications.

It has four characteristics:

> 1. Functional Programming

> 2. Predictable state

> 3. Componentization

> 4. Flexible assembly



## Documentation

Language: [English](README.md) | [Chinese](README.zh.md)



## Installation

-   Initialization state, reducer, container
-   Monitor subscriptions and initiate commands

````c#
var state = CounterState.initState();
var reduer = CounterReducer.buildReducer();
var store = Creator.createStore<CounterState>(state, reducer);

store.Subscribe(() =>
{
    CounterState lastState = store.GetState();
    var output = JsonSerializer.Serialize(lastState);
    Console.WriteLine($"[Subscribe] last-state:{output}");
});

store.Dispatch(CounterActionCreator.add(1));
store.Dispatch(CounterActionCreator.minus(2));

internal class CounterReducer
{
    internal static Reducer<CounterState> buildReducer()
    {
        var map = new Dictionary<Object, Reducer<CounterState>>();
        map.Add(CounterAction.add, _add);
        map.Add(CounterAction.minus, _minus);
        return Redux.Helper.asReducers<CounterState>(map);
    }

    private static CounterState _minus(CounterState state, Action action)
    {
        CounterState? newState = state.Clone(); //clone
        newState.Count -= action.Payload;
        return newState;
    }

    private static CounterState _add(CounterState state, Action action)
    {
        CounterState? newState = state.Clone(); //clone
        newState.Count += action.Payload;
        return newState;
    }
}

internal enum CounterAction
{
    add,
    minus,
}

internal class CounterActionCreator
{
    internal static Action add(int payload)
    {
        return new Action(CounterAction.add, payload);
    }

    internal static Action minus(int payload)
    {
        return new Action(CounterAction.minus, payload);
    }
}

[Serializable]
internal class CounterState
{
    public int Count { get; set; } = 0;

    public static CounterState initState()
    {
        var state = new CounterState();
        return state;
    }
}
```
````



## Example

-   [Counter](test/redux_tests/Counter) - a simple counter demo.
-   [Composite](test/redux_tests/Composite) - a composite demo .
-   [Todo List](test/redux_tests/TodoList) - a simple todo list demo.



## License

[License](LICENSE)