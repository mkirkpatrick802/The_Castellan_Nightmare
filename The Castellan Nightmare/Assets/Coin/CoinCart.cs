using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCart : MonoBehaviour
{
    private Treasury treasury;
    private AIDestinationSetter setter;

    private void Awake()
    {
        setter = GetComponent<AIDestinationSetter>();
    }

    public void Spawned(Treasury treasury)
    {
        this.treasury = treasury;
        setter.target = treasury.transform;
    }
}
