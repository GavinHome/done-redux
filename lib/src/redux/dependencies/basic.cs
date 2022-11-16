using Redux.Adapter;
using Redux.Basic;

namespace Redux.Dependencies.Basic;

/// Representation of each dependency
public abstract class Dependent<T> //: AbstractAdapterBuilder<T>
{
    public abstract Get<Object> subGetter(Get<T> getter);

    public abstract SubReducer<T> createSubReducer();
}

public class Dependencies<T>
{
    IDictionary<String, Dependent<T>>? slots;
    Dependent<T>? adapter;

    public Dependencies(IDictionary<String, Dependent<T>>? slots, Dependent<T> adapter)
    {
        this.slots = slots;
        this.adapter = adapter;
    }

    public Dependencies(IDictionary<String, Dependent<T>>? slots)
    {
        this.slots = slots;
    }
    public Dependencies(Dependent<T> adapter)
    {
        this.adapter = adapter;
    }

    public Reducer<T> createReducer()
    {
        List<SubReducer<T>> subs = new List<SubReducer<T>>();
        if (slots != null && slots.Any())
        {
            subs.AddRange(slots.Values.Select((Dependent<T> entry) => entry.createSubReducer()).ToList());
        }

        if (adapter != null)
        {
            subs.Add(adapter.createSubReducer());
        }

        var subReduces = Redux.Reducer.combineSubReducers(subs);
        return Reducer.combineReducers(new List<Reducer<T>> { subReduces });
    }

    public Dependent<T> slot(String type) => slots[type];

    public Dependencies<T>? trim() => (adapter != null || (slots?.Any() ?? false)) ? this : null;
}

public delegate Dependent<T> IndexedDependentBuilder<T>(int index);

public class DependentArray<T>
{
    public IndexedDependentBuilder<T> builder;
    public int length;

    public DependentArray(IndexedDependentBuilder<T> builder, int length)
    {
        this.builder = builder;
        this.length = length;
    }

    public Dependent<T> Get(int index) => builder(index);
}

public class FlowDependencies<T>
{
    public FlowAdapterView<T> build;

    public FlowDependencies(FlowAdapterView<T> build)
    {
        this.build = build;
    }

    public Reducer<T> createReducer() => (T state, Redux.Basic.Action action) =>
    {
        T copy = state;
        bool hasChanged = false;
        DependentArray<T> list = build(state);
        if (list != null)
        {
            for (int i = 0; i < list.length; i++)
            {
                Dependent<T> dep = list.Get(i);
                SubReducer<T>? subReducer = dep?.createSubReducer();
                if (subReducer != null)
                {
                    copy = subReducer(copy, action, hasChanged);
                    hasChanged = hasChanged || !EqualityComparer<T>.Default.Equals(copy, state); //copy != state;
                }
            }
        }
        return copy;
    };
}
