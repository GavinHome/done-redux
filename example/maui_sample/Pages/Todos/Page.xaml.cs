using example.Pages.Counter;
using example.Pages.Todos.FlowAdapter;
using example.Pages.Todos.TodoComponent;
using Redux;

namespace example.Pages.Todos;

public partial class Page : ContentPage
{
	public Page()
	{
		InitializeComponent();

        this.BindingContext = new TodoListViewModel();
	}
}


internal class TodoListViewModel : ViewModelUpdater<TodoListState>
{
    public TodoListViewModel()
    {
        AddCountCommand = new Command(() =>
        {
            //effect 
            // todo: call add page and return todo state;
            var todo = new ToDoState("2", "2", "2", true);
            this.Dispatch(TodoListAdapterActionCreator.add(todo));
        });

        this.Dispatch(ToDoListActionCreator.initToDos(this.initData()));
    }

    public System.Windows.Input.ICommand AddCountCommand { get; private set; }
    protected override Dependencies<TodoListState> Dependencies =>
        new Redux.Dependencies<TodoListState>(
            //slots: new Dictionary<string, Redux.Dependent<TodoListState>>()
            //{
            //    { "report",  new ReportConnector().add(new ReportComponent())  },
            //},
            adapter: new Redux.Connector.NoneConn<TodoListState>().add(Adapter.adapter)
        );
    protected override Reducer<TodoListState> reducer() => TodoListReducer.buildReducer();
    protected override TodoListState initState() => TodoListState.initState(null);
    protected override void NotifyChange()
    {
        //
    }

    /// mock data
    private List<TodoComponent.ToDoState> initData() =>
        new List<TodoComponent.ToDoState>() {
            new TodoComponent.ToDoState("1", "1", "1", false)
        };
}


