using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PlayerDeathEvent : UnityEvent<bool> {} 

public class PlayerManager : MonoBehaviour
{

    public PlayerDeathEvent playerDeathEvent = null;

    void OnPlayerDeath() {
        playerDeathEvent.Invoke(true);
    }
}
