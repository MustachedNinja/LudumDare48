using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Ground Check")]
    [Space]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;

    [Header("Crouch Check")]
    [Space]
    [Range(0, 1)] [SerializeField] private float crouchSpeed = 0.4f;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Collider crouchDisableCollider;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> {}
    public BoolEvent OnCrouchEvent;
    private bool wasCrouching = false;

    private bool crouch = false;

    Rigidbody rb = null;
    private Vector3 velocity = Vector3.zero;

    private Vector2 direction = Vector2.zero;
    private bool isGrounded = true;

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
        Move(direction * movementSpeed);
    }

    private void Move(Vector2 direction) {
        Vector2 targetDirection = direction;
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

        controller.Move(new Vector3(targetDirection.x, 0, targetDirection.y) * movementSpeed * Time.deltaTime);

        // Uncomment if we ever need gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


}
