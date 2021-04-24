using UnityEngine;
using UnityEngine.Events;

class Fridge : MonoBehaviour {
    public Transform DoorTransform;
    public float DoorOpenAngle = 135;
    public float SmoothTime = 1f;
    public float AngleEpsilon = 1f;

    public UnityEvent OnDoorOpen;
    public UnityEvent OnDoorClose;

    private float m_DoorCloseAngle;
    private float m_DoorTargetAngle;
    private float m_DoorAngularVel;

    private bool m_DoorState = false;
    private bool m_DoorRotating = false;

    public bool DoorState => this.m_DoorState;
    public bool IsDoorRotating => this.m_DoorRotating;
    public float DoorCloseAngle => this.m_DoorCloseAngle;
    public float DoorAngularVelocity => this.m_DoorAngularVel;
    public float DoorTargetAngle => this.m_DoorTargetAngle;

    private void Awake() {
        this.m_DoorCloseAngle = this.DoorTransform.localEulerAngles.y;
    }

    public void Update() {
        if(this.m_DoorRotating) {
            //apply smoothdampen to door.
            float currentAngle = this.DoorTransform.localEulerAngles.y;
            float smoothedAngle = Mathf.SmoothDampAngle(currentAngle, this.m_DoorTargetAngle, ref this.m_DoorAngularVel, this.SmoothTime);
            this.DoorTransform.localRotation = Quaternion.AngleAxis(smoothedAngle, this.DoorTransform.up);

            //check if the target angle reached by calculating delta between target and current angle. 
            if(Mathf.Abs(this.m_DoorTargetAngle - currentAngle) <= this.AngleEpsilon) {
                //if the door is already opened, invoke door close event and set door state to false.
                if(this.m_DoorState) {
                    this.OnDoorClose.Invoke();
                    this.DoorTransform.localRotation = Quaternion.AngleAxis(this.m_DoorCloseAngle, this.DoorTransform.up);
                    this.m_DoorState = false;
                } else {
                    //if the door is already closed, invoke door open event and set door state to true.
                    this.OnDoorOpen.Invoke();
                    this.DoorTransform.localRotation = Quaternion.AngleAxis(this.DoorOpenAngle, this.DoorTransform.up);
                    this.m_DoorState = true;
                }
                this.m_DoorRotating = false;
                return;
            }
        }
    }

    [ContextMenu("SwitchDoorState")]
    public void SwitchDoorState() {
        if(this.m_DoorState) {
            this.CloseDoor();
        } else {
            this.OpenDoor();
        }
    }

    [ContextMenu("OpenDoor")]
    public void OpenDoor() {
        this.m_DoorTargetAngle = this.DoorOpenAngle;
        this.m_DoorRotating = true;
    }

    [ContextMenu("CloseDoor")]
    public void CloseDoor() {
        this.m_DoorTargetAngle = this.m_DoorCloseAngle;
        this.m_DoorRotating = true;
    }
}
