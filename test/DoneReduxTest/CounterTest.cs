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
        public async Task ReducerTest()
        {
            var state = CounterState.initState();
            var store = Store<CounterState>.createStore(state, CounterReducer.buildReducer());

            await store.subscribe(async () =>
            {
                CounterState lastState = await store.getState();
                Console.WriteLine($"count:{(lastState.Counter)}");
            });

            await store.dispatch(CounterActionCreator.add(1));
            await store.dispatch(CounterActionCreator.minus(2));

            //await store.dispatch(CounterActionCreator.onCompute);

            Assert.IsTrue(state.Counter == 0);
            Assert.IsTrue((await store.getState()).Counter == -1);
        }
    }
}