using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

class Fridge : MonoBehaviour {
    public Door FridgeDoor;
    public List<Transform> Items;

    public UnityEvent OnItemTaken;
    public UnityEvent OnItemPlaced;

    private int m_CurrentItemCount;

    public int CurrentItemCount => m_CurrentItemCount;

    private void Awake() {
        m_CurrentItemCount = Items.Count;
    }

    [ContextMenu("TakeItem")]
    public void TakeItem() {
        if(FridgeDoor.State) {
            m_CurrentItemCount--;
            if(m_CurrentItemCount < 0) {
                m_CurrentItemCount = 0;
            }
            UpdateItems();
            OnItemTaken.Invoke();
        }
    }

    [ContextMenu("PlaceItem")]
    public void PlaceItem() {
        if(FridgeDoor.State) {
            m_CurrentItemCount++;
            if(m_CurrentItemCount >= Items.Count) {
                m_CurrentItemCount = Items.Count;
            }
            UpdateItems();
            OnItemPlaced.Invoke();
        }
    }

    public void UpdateItems() {
        for(int i = 0; i < Items.Count; i++) {
            if(i >= m_CurrentItemCount) {
                Items[i].gameObject.SetActive(false);
            } else {
                Items[i].gameObject.SetActive(true);
            }
        }
    }

    public void SwitchState() {
        if(!FridgeDoor.State) {
            OpenDoor();
        } else {
            CloseDoor();
        }
    }

    public void OpenDoor() => FridgeDoor.Open();

    public void CloseDoor() => FridgeDoor.Close();
}
