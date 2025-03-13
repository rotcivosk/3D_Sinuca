using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject pausePanel;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (pausePanel.activeInHierarchy) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    public void StartGame() {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void PauseGame() {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame() {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("InitialScene");
    }
}
