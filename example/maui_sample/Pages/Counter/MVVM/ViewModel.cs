using Redux;

namespace example.Pages.Counter.MVVM
{
    public class CounterViewModel : PropertyUpdater
    {
        private Store<CounterState> _store { get; set; }

        public CounterViewModel()
        {
            State = CounterState.initState();
            _store = StoreCreator.createStore<CounterState>(State, CounterReducer.buildReducer());
            _store.Subscribe(() => State = _store.GetState());
            AddCountCommand = new Command(() =>
            {
                _store.Dispatch(CounterActionCreator.add(1));
            });
        }

        private CounterState _state;

        public CounterState State { get => _state; private set => SetState(ref _state, value); }

        public System.Windows.Input.ICommand AddCountCommand { get; private set; }
    }
}
