using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

class PlayerInteraction : MonoBehaviour {
    public float Range = 2f;
    public UnityEvent OnActivation;
    public UnityEvent<string, bool> OnHoverObject;

    public void Update() {
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        if(Physics.Raycast(ray, out var hit, this.Range)) {
            var AObj = hit.collider.GetComponent<ActivatableObject>();

            if(AObj) {
                OnHoverObject.Invoke(AObj.ObjectName, AObj.Activateable);
            }
        }
    }

    [ContextMenu("RayCastInteract")]
    public void RayCastInteract() {
        if(Physics.Raycast(this.transform.position, this.transform.forward, out var hit, this.Range)) {
            var AObj = hit.collider.GetComponent<ActivatableObject>();

            if(AObj != null && AObj.Activateable) {
                OnActivation.Invoke();
                AObj.Activate();
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * this.Range);
    }
}
