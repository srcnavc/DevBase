using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IsPaid : MonoBehaviour, IFlyingTransactionBrigde
{
    [SerializeField] UnityEvent CanPayEvent;
    [SerializeField] UnityEvent CantPayEvent;
    [SerializeField] UnityEvent PaidEvent;
    [SerializeField] UnityEvent NotPaidEvent;
    [Tooltip("If It leaves null, It gets ICanBeBought interface from parent.")]
    [SerializeField] BuyProperty buyProperty;
    [SerializeField] CurrencySC RelatedCurrency;
    
    CurrencyContainer targetCurrencyContainer;
    ICanBeBought canBought;
    bool isPaid;
    bool canPay;

    public int RemainingCost { get => canBought.RemainingCost; set => canBought.RemainingCost = value; }

    public int TotalCost => canBought.Cost;

    public CurrencySC Currency => canBought.RelatedCurrency;

    private void Start()
    {
        targetCurrencyContainer = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrencyContainer>();

        if (canBought == null)
            canBought = GetComponentInParent<ICanBeBought>();

        if (canBought == null)
            Debug.Log("There is no ICanBought in parent.");
    }

    public void HasMoney()
    {
        canPay = targetCurrencyContainer.GetCurrencyValue(canBought.RelatedCurrency.Id) >= canBought.Cost;

        if (canPay)
            CanPayEvent?.Invoke();
        else
            CantPayEvent?.Invoke();
    }

    public void Pay()
    {
        if (TotalCost == -1)
            isPaid = false;
        else
            isPaid = targetCurrencyContainer.DecreaseCurrency(Currency.Id, TotalCost);

        if (isPaid)
            PaidEvent?.Invoke();
        else
            NotPaidEvent?.Invoke();

    }
}

public interface ICanBeBought
{
    int RemainingCost { get; set; }
    int Cost { get; }
    CurrencySC RelatedCurrency { get; }
}
