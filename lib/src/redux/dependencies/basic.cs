using Redux.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Redux.Dependencies.Basic;

/// Representation of each dependency
public abstract class Dependent<T>
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

///////TODO: Task2
/////// Define a base ListAdapter which is used for ListView.builder.
/////// Many small listAdapters could be merged to a bigger one.
////class ListAdapter
////{
////    private int itemCount;
////    private System.Func<int, dynamic> itemBuilder;

////    ListAdapter(int itemCount, Func<int, dynamic> itemBuilder)
////    {
////        this.itemBuilder = itemBuilder; this.itemCount = itemCount;
////    }
////}

////abstract class AbstractAdapterBuilder<T>
////{
////    //ListAdapter buildAdapter(ContextSys<T> ctx);
////}