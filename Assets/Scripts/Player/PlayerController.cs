using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PlayerDeathEvent : UnityEvent<bool> {} 

public class PlayerController : MonoBehaviour
{
    public UnityAction onDamaged;
    public UnityAction OnPlayerDeath;

    private Health health;

    void Start() {
        health = GetComponent<Health>();
        health.OnDie += OnDie;
        health.OnDamaged += OnDamaged;
    }

    void OnDamaged(float damage, GameObject damageSource) {
        if (damageSource && !damageSource.GetComponent<PlayerController>()) {
            onDamaged?.Invoke();
        }
    }

    void OnDie() {
        OnPlayerDeath?.Invoke();
        Destroy(gameObject);
    }
}
