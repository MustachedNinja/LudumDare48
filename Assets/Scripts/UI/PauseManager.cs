using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public InputManager input;
    bool isPaused = false;
    public void OnPause() {
        if (isPaused) {
            Resume();
        } else {
            Pause();
        }
    }

    public void Pause() {
        input.DisablePlayerControls();
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
    }

    public void Resume() {
        input.EnablePlayerControls();
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene(0);
    }

    void Start() {
        pausePanel.SetActive(false);
    }
}
