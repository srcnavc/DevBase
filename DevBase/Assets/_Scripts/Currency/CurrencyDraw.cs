using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CurrencyDraw : CurrencyTransactionBase
{
    public UnityEvent<int> OnDraw;
    protected override void Exhange()
    {
        if (!depositeCurrenciesAll)
        {
            OnDraw?.Invoke(container.GetCurrencyValue(currencyToExchange.Id));
            contactContainer.IncreaseCurrency(currencyToExchange.Id, container.GetCurrencyValue(currencyToExchange.Id));
            container.DecreaseCurrency(currencyToExchange.Id, container.GetCurrencyValue(currencyToExchange.Id));
        }
    }
}
