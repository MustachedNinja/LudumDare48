using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float projectileSpeed = 25f;
    public GameObject Owner { get; set; }
    Rigidbody rb;

    private float lifeTime = 4f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * projectileSpeed;

        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "Enemy") {
            OnHit(collision.collider);
            // OnHitEnemy(collision.gameObject);
        }
        Destroy(gameObject);
    }

    void OnHit(Collider collider) {
        Damageable damageable = collider.GetComponent<Damageable>();
        if (damageable) {
            damageable.InflictDamage(damage, Owner);
        }
    }

    void OnHitEnemy(Collider collider) {
        Damageable damageable = collider.GetComponent<Damageable>();
        if (damageable) {
            damageable.InflictDamage(damage, Owner);
        }
    }

    void OnHitEnemy(GameObject enemy) {
        Destroy(enemy);
    }
}
