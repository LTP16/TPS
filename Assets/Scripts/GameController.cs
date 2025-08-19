using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("=== Player Settings ===")]
    public Transform playerGun;            // Vị trí súng của player
    public GameObject bulletPrefab;        // Prefab đạn
    public float bulletSpeed = 20f;        // Tốc độ bay của đạn
    public int maxAmmo = 10;               // Số đạn tối đa mỗi băng
    public float fireRate = 0.2f;          // Tốc độ bắn
    private int currentAmmo;               // Số đạn hiện tại
    private bool isReloading = false;      // Trạng thái đang thay đạn
    public float reloadTime = 2f;          // Thời gian thay đạn
    private float nextFireTime = 1f;       // Thời gian để bắn phát tiếp theo

    [Header("=== Enemy Settings ===")]
    public GameObject enemyPrefab;         // Prefab enemy
    public Transform[] spawnPoints;        // Các điểm spawn enemy
    public int enemyCount = 1;             // Số lượng enemy spawn mỗi đợt
    public float spawnInterval = 10f;       // Thời gian giữa các lần spawn
    private float spawnTimer;

    void Start()
    {
        currentAmmo = maxAmmo; // Bắt đầu với đầy đạn
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        HandleShooting();
        HandleEnemySpawning();
    }

    // Xử lý bắn súng
    void HandleShooting()
    {
        // Bấm R để thay đạn giữa chừng
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartCoroutine(Reload());
            return;
        }

        // Nếu đang thay đạn thì không bắn được
        if (isReloading) return;

        // Bấm chuột trái để bắn
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            if (currentAmmo > 0)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
            else
            {
                Debug.Log("Hết đạn! Nhấn R để thay băng đạn.");
            }
        }
    }

    // Hàm bắn
    void Shoot()
    {
        currentAmmo--;

        GameObject bullet = Instantiate(bulletPrefab, playerGun.position, playerGun.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = playerGun.forward * bulletSpeed;

        // Xóa đạn sau 3 giây
        Destroy(bullet, 3f);
    }

    // Coroutine thay đạn
    System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Đang thay đạn...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        Debug.Log("Đã thay đạn xong!");
        isReloading = false;
    }

    // Xử lý spawn enemy
    void HandleEnemySpawning()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                enemy.AddComponent<Enemy>(); // Gán script Enemy vào
            }
            spawnTimer = spawnInterval;
        }
    }
}

// Script Enemy xử lý bị tiêu diệt
public class Enemy : MonoBehaviour
{
    public float health = 100f; // Máu của enemy

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 25f; // Mỗi viên đạn trừ 25 máu
            Destroy(collision.gameObject); // Xóa đạn khi va chạm

            if (health <= 0)
            {
                Destroy(gameObject); // Xóa enemy khi máu <= 0
            }
        }
    }
}
