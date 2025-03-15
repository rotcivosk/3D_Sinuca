using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject pausePanel;
    public static SceneController Instance { get; private set; }


    void Awake()
    {
        // Singleton
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }     
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (pausePanel.activeInHierarchy) {
                Cursor.visible = false; // Esconde o cursor
                Cursor.lockState = CursorLockMode.Locked; // Trava o cursor no centro da tela
                ResumeGame();
            } else {
                Cursor.visible = true; // Mostra o cursor
                Cursor.lockState = CursorLockMode.None; // Libera o cursor
                PauseGame();
            }
        }
    }

    public void StartGame() {
        Debug.Log("Iniciando o jogo...");
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
        SceneManager.LoadScene("MainScene");
        pausePanel.SetActive(false);
        Time.timeScale = 1f;

    }

    public void ExitToMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("InitialScene");
    }
}
