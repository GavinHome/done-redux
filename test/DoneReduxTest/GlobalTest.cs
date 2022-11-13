using DoneRedux.utils;
using DoneReduxTest.Counter;
using DoneReduxTest.Message;
using Redux.Framework;
using Redux.Utils;
using Action = Redux.Framework.Action;

namespace DoneReduxTest
{
    public class GlobalTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task CombineReducerTest()
        {
            var state = GlobalState.initState();
            var reducers = buildReducer();
            
            var store = Store<GlobalState>.createStore(state, reducers);

            await store.subscribe(async () =>
            {
                GlobalState lastState = await store.getState();
                Console.WriteLine($"count:{(lastState.Counter.Counter)}");
            });

            await store.subscribe(async () =>
            {
                GlobalState lastState = await store.getState();
                Console.WriteLine($"id:{(lastState.Message.id)}, content:{lastState.Message.content}");
            });

            await store.dispatch(CounterActionCreator.add(1));
            await store.dispatch(CounterActionCreator.minus(2));
            await store.dispatch(MessageActionCreator.modify(new MessageState(1, "helloword")));

            Assert.IsTrue(state.Counter.Counter == 0);
            Assert.IsTrue(state.Message.id == 0 && state.Message.content == "test");

            Assert.IsTrue((await store.getState()).Counter.Counter == -1);
            Assert.IsTrue((await store.getState()).Message.id == 1 && (await store.getState()).Message.content == "helloworld");
        }

        Reducer<GlobalState> buildReducer()
        {
            return null;
            //var map = new Dictionary<string, dynamic>();
            //map.Add("Counter", CounterReducer.buildReducer());
            //map.Add("Message", MessageReducer.buildReducer());

            //return Redux.Framework.ReduxHelper.combineReducers<GlobalState>(map);
        }
    }

    class GlobalState
    {
        public CounterState? Counter { get; set; }
        public MessageState? Message { get; set; }

        public static GlobalState initState()
        {
            return new GlobalState()
            {
                Counter = CounterState.initState(),
                Message = MessageState.initState()
            };
        }
    }
}