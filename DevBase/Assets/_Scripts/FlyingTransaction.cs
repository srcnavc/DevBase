using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class FlyingTransaction : MonoBehaviour
{
    public UnityEvent OnTransactionCompleted;
    [SerializeField] Transform targetTransform;
    [SerializeField] Transform parentTransform;
    [SerializeField] float exchangeSpeed;
    [SerializeField] float startScale = 1f;
    [SerializeField] float endScale = 1f;
    [SerializeField] bool isPlayerInExchangeArea;
    [SerializeField] bool dontUseCurrencyForStorage;
    [SerializeField] CurrencySC relatedCurrency;
    [SerializeField] PoolInfo stackPoolInfo;
    [SerializeField] TMP_Text costText;
    
    int forCount;
    GameObject tempGO;
    Stack tempStack;
    IFlyingTransactionBrigde brigde;
    float calculatedExcSpeed;

    private void Awake() => brigde = GetComponent<IFlyingTransactionBrigde>();

    private bool AddItemToPile
    {
        get => isPlayerInExchangeArea; set
        {
            isPlayerInExchangeArea = value;
            if (!isPlayerInExchangeArea)
                StopCoroutine(AddItemToPileFromPlayer());
            else
                StartCoroutine(AddItemToPileFromPlayer());
        }
    }

    public void StartTransfer()
    {
        if(brigde == null)
            brigde = GetComponent<IFlyingTransactionBrigde>();

        relatedCurrency = brigde.Currency;
        CalculateExcSpeed();
        
        AddItemToPile = true;
    }

    private void CalculateExcSpeed()
    {
        //It seems that casting as a float is redundant but it is not. It returns integer
        //if not casting as a float.
        calculatedExcSpeed = ((float)exchangeSpeed / (float)brigde.RemainingCost) * ((float)brigde.RemainingCost / (float)brigde.TotalCost);
        
        if (calculatedExcSpeed <= 0.1f)
        {
            float tempfloat = calculatedExcSpeed / 0.1f;
            tempfloat = 1 / tempfloat;
            forCount = (int)tempfloat;
            calculatedExcSpeed = 0.1f;
        }
        else
            forCount = 1;
    
    }

    public void StopTransfer() => AddItemToPile = false;
    
    private IEnumerator AddItemToPileFromPlayer()
    {
        CurrencyContainer tempCur = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrencyContainer>();
        
        while (AddItemToPile && tempCur.GetCurrencyValue(relatedCurrency.Id) > 0 && brigde.RemainingCost > 0)
        {
            for (int i = 0; i < forCount && AddItemToPile && tempCur.GetCurrencyValue(relatedCurrency.Id) > 0 && brigde.RemainingCost > 0; i++)
            {
                if (!dontUseCurrencyForStorage)
                    relatedCurrency.Value++;

                tempCur.DecreaseCurrency(relatedCurrency.Id, 1);

                tempGO = PoolManager.Fetch(stackPoolInfo.PoolName);
                tempGO.transform.parent = parentTransform;


                tempGO.transform.localPosition = parentTransform.InverseTransformPoint(tempCur.transform.position);

                tempGO.transform.localRotation = targetTransform.localRotation;
                tempStack = tempGO.GetComponent<Stack>();

                tempStack.TargetLocalPosition = parentTransform.InverseTransformPoint(targetTransform.position);

                tempStack.StartMovingWithScaling(Vector3.one * startScale, Vector3.one * endScale);
                tempGO.SetActive(true);


                brigde.RemainingCost--;
                UpdateUI();
            }
            
            
            yield return new WaitForSeconds(calculatedExcSpeed);
        }

        if(brigde.RemainingCost <= 0)
        {
            StopTransfer();
            OnTransactionCompleted?.Invoke();
        }
    }

    private void UpdateUI()
    {
        if (costText != null)
            costText.text = brigde.RemainingCost.ToString();
    }
}

public interface IFlyingTransactionBrigde
{
    int RemainingCost { get; set; }
    int TotalCost { get; }
    CurrencySC Currency { get; }

}
