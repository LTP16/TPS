using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health Settings")]
    public int maxHealth = 100;         // Máu tối đa
    private int currentHealth;          // Máu hiện tại

    [Header("UI")]
    public Slider healthSlider;         // Thanh máu

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    // Hàm nhận sát thương từ Enemy
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Cập nhật UI
        healthSlider.value = currentHealth;

        // Nếu hết máu
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player has died!");

        // ✅ Gọi GameManager → Player thua
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayerLose();
        }

        // Tuỳ ý: huỷ Player sau khi chết
        Destroy(gameObject);
    }
}
