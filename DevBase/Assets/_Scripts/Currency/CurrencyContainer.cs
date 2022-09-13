using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyContainer : VariableContainerBase<int, string>
{
    CurrencySC tempCurrency;
    private CurrencySC GetCurrencySC(string id)
    {
        return (CurrencySC)GetVariable(id);
    }

    public int GetCurrencyValue(string id)
    {
        if (!Contains(id))
            return -1;
        else
            return GetCurrencySC(id).Value;
    }
    public bool Contains(string id)
    {
        if (GetVariable(id) != null)
            return true;
        else
            return false;
    }
    public bool IncreaseCurrency(string id, int amount)
    {
        tempCurrency = GetCurrencySC(id);
        if (tempCurrency != null)
        {
            tempCurrency.Value += amount;
            return true;
        }
        else
            return false;
        
    }

    public bool DecreaseCurrency(string id, int amount)
    {
        tempCurrency = GetCurrencySC(id);
        if (GetCurrencySC(id) != null)
        {
            if (tempCurrency.Value >= amount)
            {
                tempCurrency.Value -= amount;
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
}
