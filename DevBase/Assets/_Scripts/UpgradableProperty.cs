using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UpgradableProperty : LevelControllerBase, ICanBeBought
{
    public Action<UpgradableProperty> OnLevelUp;
    [SerializeField] UnityEvent onLevelUp;
    [SerializeField] int remainingCost;
    [SerializeField] int currentLevel;
    [SerializeField] string id;
    [SerializeField] AttributeSC relatedAttribute;
    [SerializeField] CurrencySC relatedCurrency;
    [SerializeField] TMP_Text text;
    public override int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public override int RemainingCost { get => remainingCost; set => remainingCost = value; }

    public override int GetGetNextLevelsCost
    {
        get
        {
            if (currentLevel  < levelData.Levels.Count )
                return levelData.Levels[currentLevel].cost;
            else
                return -1;
        }
    }

    public int Cost => GetGetNextLevelsCost;

    public CurrencySC RelatedCurrency => relatedCurrency;

    public override void NextLevel()
    {
        if (GetGetNextLevelsCost == -1)
            return;

        currentLevel++;
        relatedAttribute.Value = levelData.Levels[currentLevel - 1].value;
        RemainingCost = GetGetNextLevelsCost;
        PlayerPrefs.SetInt(id + "_Level", currentLevel);
        UpdateUI();
        onLevelUp?.Invoke();
        OnLevelUp?.Invoke(this);
    }

    public override void UpdateUI()
    {
        if (text != null)
        {
            if (GetGetNextLevelsCost != -1)
                text.text = GetGetNextLevelsCost.ToString();
            else
                text.text = "MAX";

        }
    }

    private void Awake()
    {
        currentLevel = PlayerPrefs.GetInt(id + "_Level", 1);
        RemainingCost = GetGetNextLevelsCost;

        UpdateUI();
    }
}
