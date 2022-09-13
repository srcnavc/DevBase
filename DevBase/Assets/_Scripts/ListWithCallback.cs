using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ListWithCallback<T> : System.Collections.ObjectModel.Collection<T> 
{
    public Action OnListCountChanged;
    protected override void InsertItem(int index, T item)
    {
        base.InsertItem(index, item);
        OnListCountChanged?.Invoke();
    }

    protected override void SetItem(int index, T item)
    {
        base.SetItem(index, item);
        OnListCountChanged?.Invoke();
    }

    protected override void RemoveItem(int index)
    {
        base.RemoveItem(index);
        OnListCountChanged?.Invoke();
    }

    protected override void ClearItems()
    {
        base.ClearItems();
        OnListCountChanged?.Invoke();
    }

    
}
