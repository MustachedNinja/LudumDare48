using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPoint;

    public void OnPlayerShoot() {
        Projectile bullet = Instantiate(projectilePrefab, shootPoint.position, transform.rotation);
        bullet.Owner = gameObject;
    }
}
