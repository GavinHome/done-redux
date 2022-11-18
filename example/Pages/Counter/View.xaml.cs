using Redux;

namespace example.Pages.Counter;

public partial class View : ContentPage
{
    //private Store<CounterState> _store;
    //private CounterState _counterState;

    public View()
    {
        //buildStore();
        InitializeComponent();
    }

    //private void buildStore()
    //{
    //    _store = StoreCreator.createStore<CounterState>(CounterState.initState(), CounterReducer.buildReducer());
    //    _store.Subscribe(_countChangeCallback);
    //}

    //private void _countChangeCallback()
    //{
    //    CounterBtn.Text = _store.GetState().Message;
    //    SemanticScreenReader.Announce(CounterBtn.Text);
    //}

    //private void OnCounterClicked(object sender, EventArgs e)
    //{
    //    _store.Dispatch(CounterActionCreator.add(1));
    //}
}

public class CounterViewModel : Updater
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
