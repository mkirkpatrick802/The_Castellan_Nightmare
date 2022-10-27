using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    [Header("Upgrade Settings")]
    [SerializeField] private Collider2D upgradeCollider;
    [SerializeField] protected int upgradeCost;
    [SerializeField] protected float levelScaling;
    protected int level = 1;

    protected virtual void Awake()
    {
        if (!upgradeCollider) return;
        upgradeCollider.isTrigger = true;
    }

    protected virtual void OnEnable()
    {
        PlayerInput.playerUpgrade += UpgradeCheck;
    }

    protected virtual void OnDisable()
    {
        PlayerInput.playerUpgrade -= UpgradeCheck;
    }

    private void UpgradeCheck(Vector2 playerPos)
    {
        if (!upgradeCollider) return;
        if (!upgradeCollider.OverlapPoint(playerPos)) return;
        UpgradeSystem();
    }

    protected virtual void UpgradeSystem() 
    {
        level++;
        Coins.coins -= upgradeCost;
    }

    protected abstract float Scaler(float value);
}
