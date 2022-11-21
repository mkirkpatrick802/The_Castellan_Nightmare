using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    private AnimationBase _animation;
    private LayerMask _mask;
    private Transform _target;
    private float _attackRadius;
    private int _damage;

    private void Awake()
    {
        _animation = GetComponent<AnimationBase>();
    }

    public void EngageCombat(Transform target, LayerMask attackableLayer, float attackRadius, int damage)
    {

        _mask = attackableLayer;
        _damage = damage;
        _target = target;
        _attackRadius = attackRadius;

        _animation.ChangeAnimationState(AnimationStates.Attack);
    }

    public void AttackHit()
    {
        if (!Physics2D.OverlapCircle(transform.position, _attackRadius, _mask)) return;
        if (_target.CompareTag("Wall"))
        {
            WallHealth.Health -= _damage;
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
        Gizmos.DrawSphere(transform.position, _attackRadius);
    }
}
