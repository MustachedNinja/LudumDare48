using UnityEngine;

public class Damageable : MonoBehaviour
{
    public Health Health { get; private set; }

    void Awake() {
        Health = GetComponent<Health>();
        if (!Health) {
            Health = GetComponentInParent<Health>();
        }
    }

    public void InflictDamage(float damage, GameObject damageSource) {
        if (Health) {
            Health.TakeDamage(damage, damageSource);
        }
    }
}
