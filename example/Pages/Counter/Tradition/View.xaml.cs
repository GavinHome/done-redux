using Redux;
namespace example.Pages.Counter.Traditon;

public partial class View : ContentPage
{
    private Store<CounterState> _store;

    public View()
    {
        buildStore();
        InitializeComponent();
    }

    private void buildStore()
    {
        _store = StoreCreator.createStore<CounterState>(CounterState.initState(), CounterReducer.buildReducer());
        _store.Subscribe(_countChangeCallback);
    }

    private void _countChangeCallback()
    {
        CounterBtn.Text = _store.GetState().Message;
        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        _store.Dispatch(CounterActionCreator.add(1));
    }
}