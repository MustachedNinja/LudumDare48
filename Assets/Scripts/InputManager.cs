using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[Serializable]
public class PauseInputEvent : UnityEvent<bool> {}
// Create custom input events here
[Serializable]
public class PlayerMoveEvent : UnityEvent<float, float> {}

public class InputManager : MonoBehaviour
{
    InputSystem controls = null;

    public PauseInputEvent pauseInputEvent = null;
    public PlayerMoveEvent playerMoveEvent = null;
    // Add reference to custom input events

    void Awake() {
        controls = new InputSystem();
    }


    // Enable all the input events
    private void OnEnable() {
        controls.Player.Enable();
        controls.Player.Move.performed += ctx => OnMovePlayer(ctx);
        controls.Player.Move.canceled += ctx => OnMovePlayer(ctx);
        // Player controls logic goes here

        controls.UI.Enable();
        // UI controls logic goes here
        controls.UI.Pause.performed += OnPause;
    }

    public void DisablePlayerControls() {
        controls.Player.Disable();
    }

    private void OnMovePlayer(InputAction.CallbackContext context) {
        Vector2 direction = context.ReadValue<Vector2>();
        playerMoveEvent.Invoke(direction.x, direction.y);
    }

    private void OnPause(InputAction.CallbackContext context) {
        pauseInputEvent.Invoke(true);
    }
}
