using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Redux;

public class Updater : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected bool SetState<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        return SetProperty<T>(ref storage, value, propertyName);
    }

    private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Object.Equals(storage, value))
            return false;

        storage = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}