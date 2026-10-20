using System;
using System.Collections;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private int _currentCurrency;

    public Action<int> OnCurrencyChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(LoadCurrencyFromSaveFile());
    }

    private IEnumerator LoadCurrencyFromSaveFile()
    {
        yield return new WaitForEndOfFrame();

        LoadCurrency();
        this.OnCurrencyChanged?.Invoke(this._currentCurrency);
    }

    public void AddCurrency(int amount)
    {
        this._currentCurrency += amount;
        SaveCurrency();
        this.OnCurrencyChanged?.Invoke(this._currentCurrency);
    }

    public void DeductCurrency(int amount)
    {
        this._currentCurrency -= amount;
        SaveCurrency();
        this.OnCurrencyChanged?.Invoke(this._currentCurrency);
    }

    private void SaveCurrency()
    {
        PlayerPrefs.SetInt("Currency", this._currentCurrency);
    }

    private void LoadCurrency()
    {
        this._currentCurrency = PlayerPrefs.GetInt("Currency", 0);
    }
}