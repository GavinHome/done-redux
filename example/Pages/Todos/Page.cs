using example.Pages.Counter;
using Redux;

namespace example.Pages.Todos;

public class Page : ContentPage
{
    public Page()
    {
        Content = new VerticalStackLayout
        {
            Children = {
                new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to Todos Page!"
                }
            }
        };
    }
}

internal class CounterViewModel : Updater
{
    private Store<TodoListState> _store { get; set; }

    public CounterViewModel()
    {
        State = TodoListState.initState();
        _store = StoreCreator.createStore<TodoListState>(State, TodoListReducer.buildReducer());
        _store.Subscribe(() => State = _store.GetState());
        AddCountCommand = new Command(() =>
        {
            _store.Dispatch(CounterActionCreator.add(1));
        });
    }

    private TodoListState _state;

    public TodoListState State { get => _state; private set => SetState(ref _state, value); }

    public System.Windows.Input.ICommand AddCountCommand { get; private set; }
}