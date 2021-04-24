using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[Serializable]
public class PauseInputEvent : UnityEvent<bool> {}
// Create custom input events here

public class InputManager : MonoBehaviour
{
    InputSystem controls = null;

    public PauseInputEvent pauseInputEvent = null;
    // Add reference to custom input events

    void Awake() {
        controls = new InputSystem();
    }


    // Enable all the input events
    private void OnEnable() {
        controls.Player.Enable();
        // Player controls logic goes here

        controls.UI.Enable();
        // UI controls logic goes here
        controls.UI.Pause.performed += OnPause;
    }

    public void DisablePlayerControls() {
        controls.Player.Disable();
    }


    private void OnPause(InputAction.CallbackContext context) {
        pauseInputEvent.Invoke(true);
    }
}
