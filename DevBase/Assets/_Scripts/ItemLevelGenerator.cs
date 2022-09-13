using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLevelGenerator : MonoBehaviour
{
    [SerializeField] BaseLevelSC willBeModified;
    [SerializeField] int levelLimit;
    [Header("Cost Properties")]
    [SerializeField] AnimationCurve costCurve;
    [SerializeField] int costStart;
    [SerializeField] float costCurveMultiplier;
    [SerializeField] bool isCostSeries = false;
    [SerializeField] float costDifference;

    [Header("Value Properties")]
    [SerializeField] AnimationCurve valueCurve;
    [SerializeField] float valueStart;
    [SerializeField] float valueCurveMultiplier;
    [SerializeField] bool isValueSeries = false;
    [SerializeField] float valueDifference;

    ItemLevelData data;
    float tempFloat;
    float tempCost;
    float tempValue;
    public void Create()
    {
        willBeModified.Levels.Clear();
        tempCost = 0f;
        for (int i = 0; i < levelLimit; i++)
        {
            data = new ItemLevelData();
            data.level = i + 1;
            tempFloat = (float)i / (float)levelLimit;

            if (isCostSeries)
            {
                tempCost += costDifference;
                data.cost = Mathf.RoundToInt(tempCost) + costStart;
            }
            else
                data.cost = Mathf.RoundToInt(costCurve.Evaluate(tempFloat) * costCurveMultiplier) + costStart;

            if (isValueSeries)
            {
                tempValue += valueDifference;
                data.value = tempValue + valueStart;
            }
            else
                data.value = Mathf.RoundToInt(valueCurve.Evaluate(tempFloat) * valueCurveMultiplier) + valueStart;
            
            willBeModified.Levels.Add(data);
            willBeModified.Validate();
        }
    }
}
