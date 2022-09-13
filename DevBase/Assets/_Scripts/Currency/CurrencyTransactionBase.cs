using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CurrencyTransactionBase : MonoBehaviour
{
    [SerializeField] protected CurrencyContainer container;
    [SerializeField] protected CurrencySC currencyToExchange;
    [SerializeField] protected bool depositeCurrenciesAll;

    protected CurrencyContainer contactContainer;
    protected abstract void Exhange();
    private void OnCollisionEnter(Collision collision)
    {
        contactContainer = collision.transform.GetComponent<CurrencyContainer>();
        if (contactContainer != null && currencyToExchange != null && contactContainer.Contains(currencyToExchange.Id) && container.Contains(currencyToExchange.Id))
            Exhange();
    }

    private void OnTriggerEnter(Collider other)
    {
        contactContainer = other.transform.GetComponent<CurrencyContainer>();
        if (contactContainer != null && currencyToExchange != null && contactContainer.Contains(currencyToExchange.Id) && container.Contains(currencyToExchange.Id))
            Exhange();
    }
}
