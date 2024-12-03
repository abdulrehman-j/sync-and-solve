using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    // Activates the pause menu
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    // Returns to the main menu
    public void Home()
    {
        Time.timeScale = 1f; // Ensure the game is unpaused
        SceneManager.LoadScene("MainMenuScene");
    }

    // Resumes the game from pause
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }

    // Restarts the current scene
    public void Restart()
    {
        Time.timeScale = 1f; // Ensure the game is unpaused
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
