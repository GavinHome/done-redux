using DoneReduxTest.Counter;
using Redux.Framework;
using Redux.Utils;

namespace DoneReduxTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            var state = CounterState.initState();
            var store = Store<CounterState>.createStore(state);

            await store.subscribe(async () =>
            {
                CounterState cs = await store.getState();
                Console.WriteLine($"count:{(cs.Counter)}");
            });

            var newState = state.Clone();
            newState.Counter = 1;
            await store.changeState(newState);

            Assert.IsTrue((await store.getState()).Counter == 1);
        }
    }
}