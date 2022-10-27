using Pathfinding;
using Pathfinding.RVO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCart : MonoBehaviour
{
    private Treasury _treasury;
    private Transform _target;
    private AIDestinationSetter _setter;
    private bool _fromTreasury;

    public bool FromTreasury { get => _fromTreasury; }

    private void Awake()
    {
        _setter = GetComponent<AIDestinationSetter>();
    }

    public void Spawned(Treasury treasury, Transform target, bool fromTreasury)
    {
        _fromTreasury = fromTreasury;
        _treasury = treasury;
        _target = target;
        _setter.target = target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform != _target) return;
        if (!collision.CompareTag("Upgradeable")) return;

        collision.GetComponentInParent<Upgrade>().CoinDelivered();

        Destroy(gameObject);
    }
}
