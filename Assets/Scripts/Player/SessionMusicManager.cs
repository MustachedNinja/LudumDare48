using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SessionMusicManager : MonoBehaviour {
    public SoundManager manager;
    private Coroutine m_Coroutine;
    private bool m_EncounterFlag = false;

    public void OnTriggerEnter(Collider other) {
        Debug.Log(other);

        if(other.gameObject.CompareTag("Bedroom")) {
            manager.PlayClipAt(0);
        } 
        
        else if(!m_EncounterFlag && other.gameObject.CompareTag("MonsterEncounter")) {
            m_EncounterFlag = true;

            if(m_Coroutine != null) {
                StopCoroutine(m_Coroutine);
            }

            m_Coroutine = StartCoroutine(CO_PlayEncounterMusic());
        }
    }

    private IEnumerator CO_PlayEncounterMusic() {
        manager.PlayClipAt(1);
        yield return new WaitForSeconds(8);
        manager.PlayClipAt(2);
    }
}
