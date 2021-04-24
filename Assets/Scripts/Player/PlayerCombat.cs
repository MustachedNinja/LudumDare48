using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    public void OnPlayerShoot() {
        Debug.Log("PLAYER SHOOT");
        GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
        bullet.transform.parent = gameObject.transform;
    }
}
