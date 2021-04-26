using UnityEngine;
using UnityEngine.SceneManagement;
using Seftali.StateMachine;

public class PauseManager : MonoBehaviour {
    public int GameUIIndex = 0;
    public int PausePanelIndex = 1;

    public InputManager input;
    bool isPaused = false;

    public StateMachine StateMachine;
    public void OnPause() {
        if(isPaused) {
            Resume();
        } else {
            Pause();
        }
    }

    public void Pause() {
        input.DisablePlayerControls();
        StateMachine.ChangeTo(PausePanelIndex);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
    }

    public void Resume() {
        input.EnablePlayerControls();
        StateMachine.ChangeTo(GameUIIndex);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene(0);
    }
}
