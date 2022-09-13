using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CurrencyDeposit : CurrencyTransactionBase
{
    public UnityEvent<int> OnDeposite;
    protected override void Exhange()
    {
        if (!depositeCurrenciesAll)
        {
            OnDeposite?.Invoke(contactContainer.GetCurrencyValue(currencyToExchange.Id));
            container.IncreaseCurrency(currencyToExchange.Id, contactContainer.GetCurrencyValue(currencyToExchange.Id));
            contactContainer.DecreaseCurrency(currencyToExchange.Id, contactContainer.GetCurrencyValue(currencyToExchange.Id));

        }
    }
}
