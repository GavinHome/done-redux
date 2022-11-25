using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Redux;

public class PropertyUpdater : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected bool SetState<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
    {
        return SetProperty<T>(ref storage, value, propertyName);
    }

    private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
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


public abstract class ViewModelUpdater<T> : PropertyUpdater
{
    private Store<T> _store;
    public Store<T> Store => _store;
    private T? _state;

    public ViewModelUpdater()
    {
        _store = StoreCreator.createStore<T>(initState(), reducer(), Enhancer, Dependencies);

        _store.Subscribe(() =>
        {
            State = _store.GetState();
            NotifyChange();
        });
    }

    protected T State { get => _state; private set => SetState(ref _state, value); }

    protected Get<T> GetState => _store.GetState;
    protected Dispatch Dispatch => _store.Dispatch;
    protected abstract T initState();
    protected abstract Reducer<T> reducer();
    protected virtual Dependencies<T> Dependencies { get; } = null;
    protected virtual StoreEnhancer<T>? Enhancer { get; set; } = null;
    protected abstract void NotifyChange();
}