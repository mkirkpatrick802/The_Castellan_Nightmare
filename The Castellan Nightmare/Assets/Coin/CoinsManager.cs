using System;
using TMPro;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        Coins.coinsChanged += OnCoinsChanged;
        OnCoinsChanged(Coins.coins);
    }

    private void OnDisable()
    {
        Coins.coinsChanged -= OnCoinsChanged;
    }

    private void OnCoinsChanged(int obj)
    {
        _text.text = obj.ToString();
    }
}

public static class Coins
{
    public static event Action<int> coinsChanged;
    private static int _coins;

    public static int coins
    {
        get => _coins;
        set
        {
            if(_coins == value) return;
            _coins = value;
            coinsChanged?.Invoke(_coins);
        }
    }
}


