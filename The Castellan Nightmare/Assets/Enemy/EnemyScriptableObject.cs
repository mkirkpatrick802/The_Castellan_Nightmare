using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/NewEnemy", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Enemy Type")]
    public EnemyType enemyType;

    [Header("Attack Values")]
    public int damage;
    [Range(.1f, 1f)] public float attackSpeed;
    public float attackRadius;

    [Header("Other Settings")]
    public float health;
    [Range(1f, 4f)] public float maxSpeed;
    public LayerMask attackableLayers;

}

public enum EnemyType
{
    Leech,
    Launcher,
    FrontGuard,
}
