using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator _animator;
    private EnemyAnimations _currentAnimation;

    public EnemyAnimations CurrentAnimation { get => _currentAnimation; private set { } }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimation(EnemyAnimations state)
    {
        //print("Play");
        _currentAnimation = state;
        switch (state)
        {
            case EnemyAnimations.Walk:
                break;
            case EnemyAnimations.Attack:
                _animator.Play("Attack");
                break;
        }
    }

    public void AnimationDone()
    {
        _currentAnimation = EnemyAnimations.Null;
    }
}

public enum EnemyAnimations
{
    Null,
    Walk,
    Attack,
}
