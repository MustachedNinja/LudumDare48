using UnityEngine;
using UnityEngine.Events;

public class PlayerSleep : MonoBehaviour {
    public float LevelDecreaseRatio = 0.01f;

    public UnityEvent OnSleepLevelEmpty;

    private float m_SleepLevel = 1f;

    public float SleepLevel {
        get {
            return m_SleepLevel;
        }
        set {
            m_SleepLevel = Mathf.Clamp01(value);
        }
    }

    public void Update() {
        m_SleepLevel -= LevelDecreaseRatio * Time.deltaTime;
        if(m_SleepLevel <= 0) {
            m_SleepLevel = 0;
            OnSleepLevelEmpty.Invoke();
        }
    }
}
