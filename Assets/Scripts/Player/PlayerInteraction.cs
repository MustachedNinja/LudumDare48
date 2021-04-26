using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

class PlayerInteraction : MonoBehaviour {
    public Transform RayTransform;
    public float Range = 2f;
    public UnityEvent OnActivation;

    public LayerMask layermask;


    [ContextMenu("RayCastInteract")]
    public void RayCastInteract() {

        if(Physics.Raycast(this.RayTransform.position, this.RayTransform.forward, out var hit, this.Range, ~layermask)) {
            var AObj = hit.collider.GetComponent<ActivatableObject>();

            if(AObj != null && AObj.Activatable) {
                OnActivation.Invoke();
                AObj.Activate();
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.RayTransform.position, this.RayTransform.position + this.RayTransform.forward * this.Range);
    }
}
