using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;       // ✅ Máu tối đa
    private int currentHealth;        // ✅ Máu hiện tại

    [Header("Health Bar UI")]
    public Slider healthSlider;       // ✅ Slider thanh máu
    public Image fillImage;           // ✅ Ảnh Fill (thanh màu bên trong Slider)
    public Gradient healthGradient;   // ✅ Gradient chuyển màu từ xanh → đỏ

    void Start()
    {
        currentHealth = maxHealth;                     // Gán máu hiện tại = máu tối đa
        healthSlider.maxValue = maxHealth;             // Cài giá trị tối đa cho Slider
        healthSlider.value = currentHealth;            // Hiển thị đầy thanh máu
        UpdateHealthUI();                              // Cập nhật màu ban đầu
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;                       // Trừ máu
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Giữ trong khoảng [0, maxHealth]

        UpdateHealthUI();                              // Cập nhật UI

        if (currentHealth <= 0)
        {
            Die();                                     // Nếu máu = 0 → gọi Die()
        }
    }

    void UpdateHealthUI()
    {
        healthSlider.value = currentHealth;            // Cập nhật giá trị Slider

        if (fillImage != null)
        {
            float normalizedHealth = (float)currentHealth / maxHealth; // Tính % máu còn lại
            fillImage.color = healthGradient.Evaluate(normalizedHealth); // Đổi màu theo Gradient
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} has died!");

        // ✅ Nếu Enemy chết → Player thắng
        if (CompareTag("Enemy"))
        {
            GameManager.Instance.PlayerWin();
        }
        // ✅ Nếu Player chết → Player thua
        else if (CompareTag("Player"))
        {
            GameManager.Instance.PlayerLose();
        }

        Destroy(gameObject); // Xoá object sau khi chết
    }
}
