using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    void OnPlayerShoot() {
        GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
    }
}
