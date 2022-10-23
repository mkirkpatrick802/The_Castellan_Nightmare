using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [Header("Upgrade Settings")]
    [SerializeField] private CircleCollider2D upgradeCollider;
    [SerializeField] private float upgradeRadius;
    [SerializeField] protected int upgradeCost;
    protected int level;

    protected virtual void Awake()
    {
        if (!upgradeCollider) return;
        upgradeCollider.radius = upgradeRadius;
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

    protected virtual void OnDrawGizmos()
    {
        if (!upgradeCollider) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(upgradeCollider.transform.position, upgradeRadius);
    }
}
