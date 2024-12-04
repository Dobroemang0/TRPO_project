using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float stoppingDistance = 1f;
    public int health = 3;
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("»грок с тегом 'Player' не найден!");
        }
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance > stoppingDistance)
            {
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bool"))
        {
            TakeDamage(1);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement.IncreaseScore();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            PlayerMovement.IncreaseScore();
            Destroy(gameObject);
        }
    }
}