using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject crosshair;       // ✅ Kéo Crosshair (UI Image) vào Inspector
    public GameObject pausePanel;      // ✅ Kéo Panel Pause vào Inspector

    private bool isPaused = false;

    // ================== MAIN MENU ==================
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PlayGame");
        ShowCrosshair(true);
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
        ShowCrosshair(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game...");
    }

    // ================== TRONG GAME ==================
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        ShowCrosshair(false);
    }

    // ================== PAUSE MENU ==================
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Dừng game
            pausePanel.SetActive(true);
            ShowCrosshair(false);
        }
        else
        {
            Time.timeScale = 1f; // Tiếp tục game
            pausePanel.SetActive(false);
            ShowCrosshair(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        ShowCrosshair(true);
        isPaused = false;
    }

    // ================== WIN / LOSE PANEL ==================
    public void ReplayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PlayGame");
        ShowCrosshair(true);
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        ShowCrosshair(false);
    }

    // ================== CROSSHAIR ==================
    private void ShowCrosshair(bool isShow)
    {
        if (crosshair != null)
            crosshair.SetActive(isShow);
    }

    // Khi bắt đầu scene PlayGame thì crosshair sẽ hiện
    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "PlayGame")
            ShowCrosshair(true);
        else
            ShowCrosshair(false);

        if (pausePanel != null)
            pausePanel.SetActive(false); // Ẩn pause panel lúc đầu
    }
}
