using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

class SessionManager : MonoBehaviour {
    public PlayerSleep Player;

    public ActivatableObject BedActivatable;
    public ActivatableObject FridgeActivatable;

    public int Score;

    public UnityEvent<int> OnWin;
    public UnityEvent<int> OnLose;

    private void Start() {
        Player.OnSleepLevelEmpty.AddListener(this.RaiseLose);
        BedActivatable.OnActivation.AddListener(this.RaiseWin);
        FridgeActivatable.OnActivation.AddListener(this.SecondPhase);
        ResetSession();
        Debug.Log("Starting First phase");
    }

    public void SecondPhase() {
        BedActivatable.Activatable = true;
        Debug.Log("Entering Second phase");
    }

    private void RaiseLose() {
        OnLose.Invoke(Score);
        Debug.Log("Lose Condition");
    }

    private void RaiseWin() {
        OnWin.Invoke(Score);
        Debug.Log("Win Condition");
    }

    [ContextMenu("ResetSession")]
    public void ResetSession() {
        BedActivatable.Activatable = false;
        Player.SleepLevel = 1f;
    }
}
