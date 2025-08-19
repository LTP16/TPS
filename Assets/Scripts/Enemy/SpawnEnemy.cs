using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;         // ✅ Prefab Enemy (kéo thả vào Inspector)

    [Header("Spawn Points")]
    public Transform[] spawnPoints;        // ✅ Danh sách điểm spawn

    [Header("Spawn Config")]
    public float spawnDelay = 2f;          // ✅ Thời gian delay giữa các lần spawn
    public int maxEnemies = 5;            // ✅ Số enemy tối đa

    private int spawnedEnemies = 0;        // ✅ Đếm số enemy đã spawn

    void Start()
    {
        // Lặp lại hàm Spawn() sau mỗi khoảng spawnDelay
        InvokeRepeating(nameof(Spawn), 1f, spawnDelay);
    }

    void Spawn()
    {
        if (spawnedEnemies >= maxEnemies) return; // Không spawn quá giới hạn

        if (spawnPoints.Length == 0) return; // Nếu chưa có spawn points thì thoát

        // Chọn ngẫu nhiên 1 điểm spawn
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Tạo enemy tại vị trí spawn
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        spawnedEnemies++;
    }
}
