using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    public static event Action<(Transform, int)> requestingCoinCarts;

    [Header("Upgrade Settings")]
    [SerializeField] private GameObject upgradeObj;
    [SerializeField] private Collider2D upgradeCollider;
    [SerializeField] protected int upgradeCost;
    [SerializeField] protected float levelScaling;
    protected int level = 1;
    private bool _beingUpgraded;
    private int _requiredCoins;

    protected virtual void Awake()
    {
        if(!upgradeObj) return;
        upgradeObj.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        PlayerInput.playerUpgradeCheck += UpgradeCheck;
        Coins.coinsChanged += CoinsUpdated;
    }

    protected virtual void OnDisable()
    {
        PlayerInput.playerUpgradeCheck -= UpgradeCheck;
        Coins.coinsChanged -= CoinsUpdated;
    }

    private void CoinsUpdated(int coins)
    {
        if(coins >= upgradeCost)
        {
            upgradeObj.SetActive(true);
        }
        else
        {
            if (_beingUpgraded) return;
            upgradeObj.SetActive(false);
        }
    }

    private void UpgradeCheck(Vector2 playerPos)
    {
        if (!upgradeCollider) return;
        if (_beingUpgraded) return;
        if (!upgradeCollider.OverlapPoint(playerPos)) return;

        _beingUpgraded = true;
        _requiredCoins = upgradeCost;
        (Transform, int) request = (upgradeObj.transform, _requiredCoins);
        requestingCoinCarts?.Invoke(request);
    }

    public void CoinDelivered()
    {
        _requiredCoins--;
        if (_requiredCoins > 0) return;

        UpgradeSystem();
        _beingUpgraded = false;
        CoinsUpdated(Coins.coins);
    }

    protected virtual void UpgradeSystem()
    {
        level++;
    }

    protected abstract float Scaler(float value);
}
