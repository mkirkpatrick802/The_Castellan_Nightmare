
using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private AIDestinationSetter _setter;

    private void Awake()
    {
        _setter = GetComponent<AIDestinationSetter>();
    }

    public void SetTarget(Transform newTarget)
    {
        _setter.target = newTarget;
    }
}
