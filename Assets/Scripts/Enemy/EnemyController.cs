using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float attackDuration;
    public UnityAction onDamaged;
    private EnemyPathfinding pathfinding;
    private EnemyCombat combat;

    private Health health;

    void Start() {
        pathfinding = GetComponent<EnemyPathfinding>();
        pathfinding.OnPlayerReached += AttackPlayer;
        combat = GetComponent<EnemyCombat>();
        health = GetComponent<Health>();
        health.OnDie += OnDie;
        health.OnDamaged += OnDamaged;
    }

    void OnDamaged(float damage, GameObject damageSource) {
        if (damageSource && !damageSource.GetComponent<EnemyController>()) {
            onDamaged?.Invoke();
        }
    }

    void OnDie() {
        Destroy(gameObject);
    }

    public void AttackPlayer() {
        pathfinding.StopMovement(attackDuration);
        combat.OnEnemyAttack();
    }
}
