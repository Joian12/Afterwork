using System;
using TMPro;
using UnityEngine;

public class CurrencyUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currencyText;

    private void Start()
    {
        CurrencyManager.Instance.OnCurrencyChanged += UpdateCurrencyText;
    }
    
    private void OnDisable()
    {
        // just dereg from the event
        CurrencyManager.Instance.OnCurrencyChanged -= UpdateCurrencyText;
    }
    
    private void UpdateCurrencyText(int currency)
    {
        this._currencyText.text = currency.ToString();
    }
}
