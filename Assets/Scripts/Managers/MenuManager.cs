using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame() {
        //SceneManager.LoadScene(1);  // Index of Level01
        SceneManager.LoadScene(2);  // Index of Level02
    }

    public void QuitGame() {
        // save any game data here
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
