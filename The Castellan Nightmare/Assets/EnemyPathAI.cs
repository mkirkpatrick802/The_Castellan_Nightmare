using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathAI : AIPath
{
    private EnemyController _controller;

    protected override void Awake()
    {
        base.Awake();
        _controller = GetComponent<EnemyController>();
    }

    public override void OnTargetReached()
    {
        _controller.TargetReached();
    }
}
