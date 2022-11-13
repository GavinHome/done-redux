using DoneReduxTest.Counter;
using Redux.Framework;
using Redux.Utils;
using Action = Redux.Framework.Action;

namespace DoneReduxTest
{
    public class CounterTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ReducerTest()
        {
            var state = CounterState.initState();
            var enhancers = Redux.Framework.Middleware.applyMiddleware<CounterState>(
                Redux.Middleware.logMiddleware<CounterState>((state) => "counterstate", true, "CounterPage"),
                Redux.Middleware.exceptionMiddleware<CounterState>("CounterPage"),
                Redux.Middleware.performanceMiddleware<CounterState>(true, "CounterPage")
            );
            var store = Store<CounterState>.createStore(state, CounterReducer.buildReducer(), enhancers);

            store.subscribe(() =>
            {
                CounterState lastState = store.getState();
                Console.WriteLine($"count:{(lastState.Counter)}");
            });

            store.dispatch(CounterActionCreator.add(1));
            store.dispatch(CounterActionCreator.minus(2));

            //await store.dispatch(CounterActionCreator.onCompute);

            Assert.IsTrue(state.Counter == 0);
            Assert.IsTrue(store.getState().Counter == -1);
        }
    }
}