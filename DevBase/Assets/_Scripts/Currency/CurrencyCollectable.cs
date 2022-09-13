using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CurrencyCollectable : MonoBehaviour
{
    public UnityEvent OnCollect;
    public CurrencySC relatedCurrency;
    public int value;
    [SerializeField] bool isAvailable = false;

    CurrencyContainer container;

    public bool IsAvailable { get => isAvailable; set => isAvailable = value; }

    private void Awake()
    {
        container = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrencyContainer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsAvailable)
            return;

        container = other.transform.GetComponent<CurrencyContainer>();

        if (container != null && container.Contains(relatedCurrency.Id))
            AddToContainer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        container = collision.transform.GetComponent<CurrencyContainer>();

        if (container != null && container.Contains(relatedCurrency.Id))
            AddToContainer();
    }

    public void Collect()
    {
        if (container != null)
            AddToContainer();
        else
            Debug.Log("Container null.");
    }

    private void AddToContainer()
    {
        container.IncreaseCurrency(relatedCurrency.Id, value);
        gameObject.SetActive(false);
        OnCollect?.Invoke();
    }
}
