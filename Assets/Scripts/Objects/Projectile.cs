using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 25f;
    Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * projectileSpeed;
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "Enemy") {
            OnHitEnemey(collision.gameObject);
        }
        Destroy(gameObject);
    }

    void OnHitEnemey(GameObject enemy) {
        Destroy(enemy);
    }
}
