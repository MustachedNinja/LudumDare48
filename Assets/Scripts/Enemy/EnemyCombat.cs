using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private Weapon weaponPrefab;
    [SerializeField] private Transform weaponSpawnPoint;
    [SerializeField] private LayerMask attackLayer;

    public void OnEnemyAttack() {
        Weapon weapon = Instantiate(weaponPrefab, weaponSpawnPoint.position, weaponSpawnPoint.rotation);
        weapon.Owner = gameObject;
        weapon.Attack(attackLayer);
    }
}
