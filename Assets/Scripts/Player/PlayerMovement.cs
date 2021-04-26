using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float turnSmoothTime = 0.1f;

    [Header("Ground Check")]
    [Space]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private LayerMask groundMask;

    [Header("Crouch Check")]
    [Space]
    [Range(0, 1)] [SerializeField] private float crouchSpeed = 0.4f;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Collider crouchDisableCollider;

    [Header("Camera")]
    [Space]
    [SerializeField] private Transform thirdPersonCam;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> {}
    public BoolEvent OnCrouchEvent;
    private bool wasCrouching = false;

    private bool crouch = false;

    Rigidbody rb = null;
    private Vector3 velocity = Vector3.zero;

    private Vector2 direction = Vector2.zero;
    private bool isGrounded = true;
    private float turnSmoothVelocity;

    public void OnMove(float x, float y) {
        direction = new Vector2(x, y);
    }


    private void Awake() {
        rb = GetComponent<Rigidbody>();
        if (OnCrouchEvent == null) {
            OnCrouchEvent = new BoolEvent();
        }
    }

    private void Update() {
        isGrounded = CheckGrounded();
        Vector3 moveDirection = Rotate(direction);
        Move(moveDirection.normalized * movementSpeed);
    }

    private Vector3 Rotate(Vector2 direction) {
        if (direction.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        } else {
            return Vector3.zero;
        }
    }

    private void Move(Vector3 direction) {
        Vector3 targetDirection = direction;
        if (crouch) {
            if (!wasCrouching) {
                wasCrouching = true;
                OnCrouchEvent.Invoke(true);
            }

            targetDirection *= crouchSpeed;

            if (crouchDisableCollider != null) {
                crouchDisableCollider.enabled = false;
            }
        } else {
            if (crouchDisableCollider != null) {
                crouchDisableCollider.enabled = true;
            }
            if (wasCrouching) {
                wasCrouching = false;
                OnCrouchEvent.Invoke(false);
            }
        }

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        controller.Move(targetDirection * movementSpeed * Time.deltaTime);

        // Uncomment if we ever need gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private bool CheckGrounded() {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }


}
