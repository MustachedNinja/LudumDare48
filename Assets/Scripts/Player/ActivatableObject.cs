using UnityEngine;
using UnityEngine.Events;

class ActivatableObject : MonoBehaviour {
    public UnityEvent OnActivation;
    public string ObjectName;
    public bool Activatable = true;

    [ContextMenu("Activate")]
    public void Activate() {
        OnActivation.Invoke();
    }
}
