using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float attackTime;
    private AIDestinationSetter _setter;
    
    private void Awake()
    {
        _setter = GetComponent<AIDestinationSetter>();
    }

    public void SetTarget(List<Transform> newTarget)
    {
        Transform closestTarget = null;
        float smallestDistance = 0;
        foreach (var target in newTarget)
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Wall")) return;
        
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackTime);
            WallHealth.Health -= damage;
            print("Wall Health is Currently: " + WallHealth.Health);
        }
        
        // ReSharper disable once IteratorNeverReturns
    }
}
