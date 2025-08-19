using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton (dùng ?? g?i ? m?i script)

    [Header("UI Panels")]
    public GameObject winPanel;   // Panel Win
    public GameObject losePanel;  // Panel Lose

    private void Awake()
    {
        // ??m b?o ch? có 1 GameManager t?n t?i
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // ? ?n c? 2 panel khi m?i b?t ??u game
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);

        // ? B? pause game n?u tr??c ?ó ?ã b? d?ng
        Time.timeScale = 1f;
    }

    public void PlayerWin()
    {
        Time.timeScale = 0f;       // D?ng game
        if (winPanel != null) winPanel.SetActive(true);  // Hi?n UI th?ng
    }

    public void PlayerLose()
    {
        Time.timeScale = 0f;       // D?ng game
        if (losePanel != null) losePanel.SetActive(true); // Hi?n UI thua
    }
}
