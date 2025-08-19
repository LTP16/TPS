using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    [Header("Enemy Settings")]
    public Transform player;           // Player target
    public float moveSpeed = 1f;
    public float stopDistance = 4f;

    [Header("Attack Settings")]
    public int damage = 2;
    public float attackCooldown = 2f;
    private float lastAttackTime;

    private PlayerHealth playerHealth;
    private Animator anim;

    void Start()
    {
        // 🔥 Nếu chưa gán player trong Inspector thì tự tìm theo Tag
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
                playerHealth = player.GetComponent<PlayerHealth>();
            }
            else
            {
                Debug.LogWarning("Không tìm thấy Player trong Scene! Gán tag 'Player' cho nhân vật.");
            }
        }
        else
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(direction);

            anim.SetBool("isWalking", true);
            anim.SetBool("isAttacking", false);
        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", true);

            TryAttackPlayer();
        }
    }

    void TryAttackPlayer()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log($"{gameObject.name} attacked Player - {damage} damage!");
            }
        }
    }
}
