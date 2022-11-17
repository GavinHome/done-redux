<h1>Done Redux</h1>

[![build](https://github.com/GavinHome/done-redux/actions/workflows/build.yml/badge.svg?branch=master)](https://github.com/GavinHome/done-redux/actions/workflows/build.yml) [![codecov](https://codecov.io/gh/gavinhome/done-redux/branch/master/graph/badge.svg)](https://codecov.io/gh/gvinhome/done-redux)



## What is Done Redux?

Done Redux is a combined state management framework based on Redux and .Net7, namely Donet Redux.
It is suitable for building .NET applications.

It has four characteristics:

> 1. Functional Programming

> 2. Predictable State

> 3. Componentization

> 4. Flexible assembly



## Documentation

Language: [English](README.md) | [Chinese](README.zh.md)



## Installation

-   Initialization state, reducer, container
-   Monitor subscriptions 
-   initiate commands

````c#
using Redux;

var state = CounterState.initState();
var reducer = CounterReducer.buildReducer();
var store = StoreCreator.createStore<CounterState>(state, reducer);

store.Subscribe(() =>
{
	var lastState = store.GetState();
	var stateJson = System.Text.Json.JsonSerializer.Serialize(lastState);
	Console.WriteLine($"[Subscribe] last-state:{stateJson}");
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
        return Converter.asReducers<CounterState>(map);
    }

    private static CounterState _minus(CounterState state, Redux.Action action)
    {
        CounterState? newState = state.Clone(); //clone
        newState.Count -= action.Payload;
        return newState;
    }

    private static CounterState _add(CounterState state, Redux.Action action)
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
    internal static Redux.Action add(int payload)
    {
        return new Redux.Action(CounterAction.add, payload);
    }

    internal static Redux.Action minus(int payload)
    {
        return new Redux.Action(CounterAction.minus, payload);
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
````



## Example

-   [Counter](test/redux_tests/Counter) - a simple counter demo.
-   [Composite](test/redux_tests/Composite) - a composite demo .
-   [Todo List](test/redux_tests/TodoList) - a simple todo list demo.



## License

[License](LICENSE)