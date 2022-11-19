using Redux;
namespace example.Pages.Counter.FluentView;
public class CounterView
{
    public static Microsoft.Maui.Controls.View buildView(CounterState state, Dispatch dispatch)
    {
        return new VerticalStackLayout
        {
            Children = {
                new Label {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Text = "Welcome to .NET MAUI!"
                }
            }
        };
    }
}