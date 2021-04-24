using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public List<AudioClip> Clips;
    public AudioSource AudioSource;
    public void PlayClipAt(int index) {
        AudioSource.clip = Clips[index];
        AudioSource.Play();
    }

    public void PlayClipRandom() => PlayClipAt(Mathf.FloorToInt(Random.value * Clips.Count));
    public void Stop() => AudioSource.Stop();

}
