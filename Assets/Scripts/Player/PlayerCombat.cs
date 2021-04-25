using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shootPoint;

    public void OnPlayerShoot() {
        GameObject bullet = Instantiate(projectile, shootPoint.position, transform.rotation);
    }
}
