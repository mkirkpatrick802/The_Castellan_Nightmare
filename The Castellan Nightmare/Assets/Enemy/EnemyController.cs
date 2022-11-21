using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject values;

    private AIDestinationSetter _setter;
    private EnemyPathAI _path;
    private EnemySpawner _spawner;
    private EnemyAttackHandler _attackHandler;
    private HealthHolder _healthHolder;
    private bool _quitting;
    private bool _isInCombat = false;
    
    public Vector2 Velocity { get => _path.velocity; private set { } }

    public bool IsInCombat { get => _isInCombat; private set { } }

    private void Awake()
    {
        _path = GetComponent<EnemyPathAI>();
        _setter = GetComponent<AIDestinationSetter>();
        _attackHandler = GetComponentInChildren<EnemyAttackHandler>();
        _path.maxSpeed = values.maxSpeed;

        _healthHolder = new HealthHolder(values.health);
    }

    public void SetSpawner(EnemySpawner spawner)
    {
        _spawner = spawner;
    }

    public void SetTarget(List<Transform> wallTargets)
    {
        Transform closestTarget = null;
        float smallestDistance = 0;

        foreach (Transform target in wallTargets)
        {
            var testDistance = Vector2.Distance(transform.position, target.position);
            if (!closestTarget) {closestTarget = target; smallestDistance = testDistance;}
            if (!(testDistance < smallestDistance)) continue;
            
            closestTarget = target;
            smallestDistance = testDistance;
        }
        
        if(!closestTarget) print("Target Null");
        _setter.target = closestTarget;
    }

    public void AttackAlly(Transform ally)
    {
        _isInCombat = true;
        _setter.target = ally;
    }

    public void TakeDamage(float takeDamage)
    {
        _healthHolder.Health -= takeDamage;
        if ( _healthHolder.Health <= 0) Destroy(gameObject);
    }

    public void TargetReached()
    {
        //print(gameObject.name + " Attacking " + _setter.target + "!!!");
        _isInCombat = true;

        //TODO: Make this function be called every x amount of time while this enemy is on its target
        _attackHandler.EngageCombat(_setter.target);
    }

    private void OnApplicationQuit()
    {
        _quitting = true;
    }

    private void OnDestroy()
    {
        if (_quitting) return;
        if(_spawner)
            _spawner.EnemyDestroyed(this);
    }
}
