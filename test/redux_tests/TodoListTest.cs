using Redux;
using Redux.Basic;
using Redux.Connector;
using Redux.Dependencies.Basic;
using System.Text.Json;
using Todo;

namespace TodoList;

public class TodoListTest
{

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test()
    {
        var state = TodoListState.initState();
        var reducers = TodoListReducer.buildReducer();

        var enhancers = Redux.Middleware.applyMiddleware<TodoListState>(
            Redux.Middlewares.logMiddleware<TodoListState>((state) => JsonSerializer.Serialize(state), "TodoListTests"),
            Redux.Middlewares.exceptionMiddleware<TodoListState>("TodoListTests"),
            Redux.Middlewares.performanceMiddleware<TodoListState>("TodoListTests")
        );

        Dependencies<TodoListState> dependencies = new Dependencies<TodoListState>(
            adapter: new NoneConn<TodoListState>().add(Adapter.adapter)
        //adapter: new NoneConn<TodoListState>() + Adapter.adapter
        );

        Store<TodoListState> store = Creator.createStore<TodoListState>(state, reducers, enhancers, dependencies);

        store.Subscribe(() =>
        {
            TodoListState lastState = store.GetState();
            Console.WriteLine($"[Subscribe] last-state:{JsonSerializer.Serialize(lastState)}");
        });

        store.Dispatch(ToDoListActionCreator.initToDos(_init()));
        Assert.IsTrue(store.GetState().toDos.Count == 3);
        Assert.IsTrue(store.GetState().toDos?.Count(x => x.IsDone) == 2);

        store.Dispatch(TodoListAdapterActionCreator.add(
                new Todo.ToDoState(
                    "3",
                    "Hello Done Redux",
                    "Learn how to use Fish Redux in a flutter application.",
                    true
                )
            )
         );

        Assert.IsTrue(store.GetState().toDos.Count == 4);
        Assert.IsTrue(store.GetState().toDos?.Count(x => x.IsDone && x.Id == "3" && x.Title == "Hello Done Redux") == 1);

        store.Dispatch(ToDoActionCreator.done("3"));
        Assert.IsTrue(store.GetState().toDos?.Count(x => x.IsDone && x.Id == "3") == 0);

        store.Dispatch(TodoListAdapterActionCreator.remove("3"));
        Assert.IsTrue(store.GetState().toDos.Count == 3);
        Assert.IsTrue(store.GetState().toDos?.Count(x => x.IsDone) == 2);
    }

    private List<Todo.ToDoState> _init()
    {
        return new List<Todo.ToDoState>()
            {
                new Todo.ToDoState(
                     id: "0",
                     title: "Hello world",
                     desc: "Learn how to program.",
                     isDone: true
                ),
                new Todo.ToDoState(
                    id: "1",
                    title: "Hello Flutter",
                    desc: "Learn how to build a flutter application.",
                    isDone: true
                 ),
                new Todo.ToDoState(
                    id: "2",
                    title: "How Fish Redux",
                    desc: "Learn how to use Fish Redux in a flutter application.",
                    isDone: false
                )
            };
    }
}