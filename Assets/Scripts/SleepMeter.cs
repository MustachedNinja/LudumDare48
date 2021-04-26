using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepMeter : MonoBehaviour {
    public Slider Slider;
    public PlayerSleep PlayerSleep;

    // Update is called once per frame
    void Update() {
        Slider.value = PlayerSleep.SleepLevel;
    }
}
