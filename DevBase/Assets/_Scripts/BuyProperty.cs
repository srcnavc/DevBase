using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class BuyProperty : MonoBehaviour, ICanBeBought
{
    public Action<BuyProperty> OnBuyProperty;
    [SerializeField] UnityEvent OnBuyBuilding;
    [SerializeField] string propertyId;
    [SerializeField] bool enableOnStart = false;
    [SerializeField] List<GameObject> itemsToDisableIfBuildingDisabled = new List<GameObject>();
    [SerializeField] GameObject buyColliderGo;
    [SerializeField] CurrencySC relatedCurrency;
    [SerializeField] int cost;
    [SerializeField] int remainingCost;
    [SerializeField] TMP_Text buyCostText;
    CurrencyContainer targetCurrencyContainer;
    public string PrefId => propertyId + "_Property";
    public int Cost => cost;
    public bool IsActive => PlayerPrefs.GetInt(PrefId, 0) == 1 || enableOnStart;
    public CurrencySC RelatedCurrency => relatedCurrency;
    public int RemainingCost { get => remainingCost; set => SetRemainingCost(value); }
    public GameObject BuyColliderGo { get => buyColliderGo; set => buyColliderGo = value; }

    private void SetRemainingCost(int remaining)
    {
        remainingCost = remaining;
        PlayerPrefs.SetInt(PrefId + "_RemaininCost", RemainingCost);
        UpdateUI();
    }

    public void DisableBuilding()
    {
        for (int i = 0; i < itemsToDisableIfBuildingDisabled.Count; i++)
            itemsToDisableIfBuildingDisabled[i].SetActive(false);
    }

    
    public void EnableBuilding()
    {
        BuyColliderGo.SetActive(false);

        for (int i = 0; i < itemsToDisableIfBuildingDisabled.Count; i++)
            itemsToDisableIfBuildingDisabled[i].SetActive(true);
        
        OnBuyBuilding?.Invoke();
        OnBuyProperty?.Invoke(this);
    }

    public void Buy()
    {
        bool isPaid = targetCurrencyContainer.DecreaseCurrency(relatedCurrency.Id, Cost);

        if (isPaid)
        {
            PlayerPrefs.SetInt(PrefId, 1);
            EnableBuilding();
        }
        else
            Debug.Log(gameObject.name + " : not enought money.");
    }

    public void GiveOwnerShip()
    {
        PlayerPrefs.SetInt(PrefId, 1);
        
        EnableBuilding();
    }

    private void Awake()
    {
        RemainingCost = PlayerPrefs.GetInt(PrefId + "_RemaininCost", Cost);

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (buyCostText != null)
            buyCostText.text = RemainingCost.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(enableOnStart)
            PlayerPrefs.SetInt(PrefId, 1);

        if (PlayerPrefs.GetInt(PrefId, 0) == 0)
            DisableBuilding();
        else
            EnableBuilding();
        
        targetCurrencyContainer = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrencyContainer>();
    }
}
