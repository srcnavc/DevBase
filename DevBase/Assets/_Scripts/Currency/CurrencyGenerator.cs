using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyGenerator : MonoBehaviour
{
    public Action OnGenerate;
    public CurrencySC currencyToGenerate;
    public float timeToGenerate = 10f;
    public int generateAmount = 1;
    public CurrencyContainer targetContainer;

    float startTime;
    bool canGenerate = true;
    private void Start()
    {
        if (currencyToGenerate == null)
            throw new System.Exception("CurrencyToGenerate is null.");
        else
            canGenerate = true;

        if (targetContainer == null)
            throw new System.Exception("targetContainer is null.");
        else
            canGenerate = true;

        
        startTime = Time.time;
    }

    private void LateUpdate()
    {
        if (startTime + timeToGenerate <= Time.time && canGenerate)
        {
            GenerateCurrency();
            startTime = Time.time;
        }
    }

    private void GenerateCurrency()
    {
        if (targetContainer.Contains(currencyToGenerate.Id))
        {
            OnGenerate?.Invoke();
            targetContainer.IncreaseCurrency(currencyToGenerate.Id, generateAmount);
        }
        else
            throw new System.Exception(currencyToGenerate.Name + " is not found in the container");
    }
}
