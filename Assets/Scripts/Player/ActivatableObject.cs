using UnityEngine;
using UnityEngine.Events;

class ActivatableObject : MonoBehaviour {
    public UnityEvent OnActivation;
    public string ObjectName;
    public bool Activateable = true;

    public void Activate() {
        OnActivation.Invoke();
    }
}
