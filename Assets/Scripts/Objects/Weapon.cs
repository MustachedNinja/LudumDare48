using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private float range = 1f;
    public GameObject Owner { get; set; }
    
    void Start() {
        Destroy(gameObject, animationDuration);
    }

    public void Attack(LayerMask target) {
        Collider[] hit = Physics.OverlapSphere(transform.position, range, target);
        if (hit.Length > 0) {
            OnHit(hit[0]);
        }
    }

    void OnHit(Collider collider) {
        Damageable damageable = collider.GetComponent<Damageable>();
        if (damageable) {
            damageable.InflictDamage(damage, Owner);
        }
    }
}
