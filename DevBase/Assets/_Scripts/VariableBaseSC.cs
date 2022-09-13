using System;
using UnityEngine;


public abstract class VariableBaseSC<T,T2> : ScriptableObject
{
    [SerializeField] private T2 id;
    [SerializeField] private T value;

    public T2 Id { get { return id; }  set { id = value; } }
    public T Value 
    {
        get
        {
            return value;
        }

        set
        {
            if (!this.value.Equals(value))
            {
                this.value = value;
                OnValueChanged?.Invoke(this.value);
            }

        }
    }
    public Action<T> OnValueChanged;
}
