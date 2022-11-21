using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject values;

    private AnimationBase _animation;
    private Transform _target;

    private void Awake()
    {
        _animation = GetComponent<AnimationBase>();
    }

    public void EngageCombat(Transform target)
    {
        _target = target;

        _animation.ChangeAnimationState(AnimationStates.Attack);
    }

    public void AttackHit()
    {
        if (!Physics2D.OverlapCircle(transform.position, values.attackRadius, values.attackableLayers)) return;
        if (_target.CompareTag("Wall"))
        {
            WallHealth.Health -= values.damage;
            print("Wall Health is Currently: " + WallHealth.Health);
        }
        else if (_target.CompareTag("Ally"))
        {
            print("Player Health is Currently: GAMER");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, values.attackRadius);
    }
}
