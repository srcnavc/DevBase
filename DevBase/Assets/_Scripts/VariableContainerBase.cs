using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VariableContainerBase<T, T2> : MonoBehaviour
{
    [SerializeField] protected List<VariableBaseSC<T,T2>> VariableList = new List<VariableBaseSC<T, T2>>();
    protected VariableBaseSC<T, T2> GetVariable(T2 id)
    {
        for (int i = 0; i < VariableList.Count; i++)
        {
            if (VariableList[i].Id.Equals(id))
                return VariableList[i];

        }
        return null;
    }
}
