using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateTextValue : MonoBehaviour
{
    [SerializeField] CurrencySC relatedCurrency;
    [SerializeField] TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        relatedCurrency.OnValueChanged += OnValueChanged;
        text.text = relatedCurrency.Value.ToString();
    }

    private void OnValueChanged(int value)
    {
        text.text = value.ToString();
    }

    private void OnDestroy()
    {
        relatedCurrency.OnValueChanged -= OnValueChanged;
    }
}
