using System;

public static class CoinsManager
{
    public static event Action<int> coinsChanged;
    private static int _coins;

    public static int Coins
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
