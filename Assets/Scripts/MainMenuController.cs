using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // ================== MAIN MENU ==================
    // Gọi khi bấm nút Play ở Main Menu
    public void StartGame()
    {
        Time.timeScale = 1f; // Đảm bảo game không bị pause
        SceneManager.LoadScene("PlayGame");
    }

    // Gọi khi bấm nút Settings ở Main Menu
    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    // Gọi khi bấm nút Exit ở Main Menu
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game..."); // Chỉ hiển thị trong Unity Editor
    }

    // ================== TRONG GAME ==================
    // Gọi khi bấm nút Back (từ PlayGame → MainMenu)
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // ================== WIN / LOSE PANEL ==================
    // Gọi khi bấm nút Replay (chơi lại)
    public void ReplayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PlayGame");
    }

    // Gọi khi bấm nút Quit (thoát hẳn game)
    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }

    // Gọi khi bấm nút Go to Main Menu (từ Win/Lose Panel → Main Menu)
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
