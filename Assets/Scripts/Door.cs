using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour {
    public Transform HingeTransform;
    public float DoorOpenAngle = 135;
    public float SmoothTime = 1f;

    public UnityEvent OnOpen;
    public UnityEvent OnClose;

    private float m_DoorCloseAngle;
    private float m_DoorAngularVel;

    private bool m_State = false;

    public bool State => this.m_State;
    public float CloseAngle => this.m_DoorCloseAngle;
    public float AngularVelocity => this.m_DoorAngularVel;

    private void Awake() {
        this.m_DoorCloseAngle = this.HingeTransform.localEulerAngles.y;
    }

    public void Update() {
        float currentAngle = this.HingeTransform.localEulerAngles.y;
        float smoothedAngle = Mathf.SmoothDampAngle(currentAngle, this.m_State ? this.DoorOpenAngle : this.m_DoorCloseAngle, ref this.m_DoorAngularVel, this.SmoothTime);

        this.HingeTransform.localRotation = Quaternion.AngleAxis(smoothedAngle, this.HingeTransform.up);
    }

    [ContextMenu("SwitchState")]
    public void SwitchState() {
        if(this.m_State) {
            this.Close();
        } else {
            this.Open();
        }
    }

    [ContextMenu("Open")]
    public void Open() {
        this.m_State = true;
        OnOpen.Invoke();
    }

    [ContextMenu("Close")]
    public void Close() {
        this.m_State = false;
        OnClose.Invoke();
    }
}
