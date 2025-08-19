using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("Cài đặt máu")]
    public float maxHealth = 100f; // Máu tối đa
    public float currentHealth;

    [Header("UI")]
    public Image healthBarFill; // Image của phần máu (Fill)

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // Hàm nhận sát thương
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Cập nhật UI thanh máu
    void UpdateHealthUI()
    {
        float fillAmount = currentHealth / maxHealth;
        healthBarFill.fillAmount = fillAmount;

        // Chuyển màu từ xanh lá (0,1,0) sang đỏ (1,0,0)
        Color newColor = Color.Lerp(Color.red, Color.green, fillAmount);
        healthBarFill.color = newColor;
    }

    void Die()
    {
        Debug.Log("Enemy đã chết!");
        Destroy(gameObject);
    }

    // Test: Ấn phím T để bắn trúng
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(20f);
        }
    }
}
