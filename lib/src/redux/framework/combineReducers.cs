using Redux.Basic;
using System;
using System.Security.Claims;

namespace Redux;

public class Reducer
{
    public static Reducer<T> combineReducers<T>(IList<Reducer<T>> reducers)
    {
        var notNullReducers = reducers?.Where((Reducer<T> r) => r != null)?.ToArray();
        if (notNullReducers == null || !notNullReducers.Any())
        {
            return null;
        }

        if (notNullReducers.Length == 1)
        {
            return notNullReducers.Single();
        }

        return (T state, Redux.Basic.Action action) =>
        {
            T nextState = state;
            foreach (Reducer<T> reducer in notNullReducers)
            {
                nextState = reducer(nextState, action);
            }

            return nextState;
        };
    }

    /// Combine an iterable of SubReducer<T> into one Reducer<T>
    public static Reducer<T> combineSubReducers<T>(IList<SubReducer<T>> subReducers)
    {
        var notNullReducers = subReducers?.Where((SubReducer<T> r) => r != null)?.ToArray();
        if (notNullReducers == null || !notNullReducers.Any())
        {
            return null;
        }

        if (notNullReducers.Length == 1)
        {
            SubReducer<T> single = notNullReducers.Single();
            return (T state, Redux.Basic.Action action) => single(state, action, false);
        }

        return (T state, Redux.Basic.Action action) =>
        {
            T copy = state;
            bool hasChanged = false;
            foreach (SubReducer<T> subReducer in notNullReducers)
            {
                copy = subReducer(copy, action, hasChanged);
                hasChanged = hasChanged || !EqualityComparer<T>.Default.Equals(copy, state); //copy != state
            }
            return copy;
        };
    }

    /// Convert a super Reducer<Sup> to a sub Reducer<Sub>
    Reducer<Sub> castReducer<Sub, Sup>(Reducer<Sup> sup) where Sub : class, Sup
    {
        return sup == null
            ? null
            : (Sub state, Redux.Basic.Action action) =>
            {
                Sub result = sup(state, action) as Sub;
                return result;
            };
    }
}
