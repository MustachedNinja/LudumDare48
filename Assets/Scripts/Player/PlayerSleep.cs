using UnityEngine;
using UnityEngine.Events;

public class PlayerSleep : MonoBehaviour {
    public float LevelIncreaseRatio = 0.1f;

    public UnityEvent OnSleepLevelFull;
    public UnityEvent OnSleepLevelEmpty;

    public bool Sleep = false;

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
        if(Sleep) {
            m_SleepLevel += LevelIncreaseRatio * Time.deltaTime;

            if(m_SleepLevel >= 1) {
                m_SleepLevel = 1;
                OnSleepLevelFull.Invoke();
                Sleep = false;
            }
        }
    }

    public void DecreaseLevel(float val) {
        m_SleepLevel -= val;
        if(m_SleepLevel <= 0) {
            m_SleepLevel = 0;
            OnSleepLevelEmpty.Invoke();
        }
    }

}
