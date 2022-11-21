using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    private EnemyAnimationController _controller;
    private LayerMask _mask;
    private Transform _target;
    private float _attackRadius;
    private int _damage;

    private void Awake()
    {
        _controller = GetComponent<EnemyAnimationController>();
    }

    public void EngageCombat(Transform target, LayerMask attackableLayer, float attackRadius, int damage)
    {
        if (_controller.CurrentAnimation == EnemyAnimations.Attack) return;

        _mask = attackableLayer;
        _damage = damage;
        _target = target;
        _attackRadius = attackRadius;
        _controller.PlayAnimation(EnemyAnimations.Attack);
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
