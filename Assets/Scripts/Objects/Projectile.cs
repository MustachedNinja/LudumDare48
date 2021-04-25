using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 25f;
    Rigidbody rb;

    private float lifeTime = 4f;
    private float timeAlive;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * projectileSpeed;
        timeAlive = 0f;
    }

    void Update() {
        timeAlive += Time.deltaTime;
        if (timeAlive > lifeTime) {
            Destroy(gameObject);
        }
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
