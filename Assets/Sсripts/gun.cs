using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float moveSpeed = 2f;
    public float stoppingDistance = 1f;
    public float fireRate = 2f;
    private float nextFireTime = 0f;
    public float bulletSpawnOffset = -1f;
    public int health = 3;

    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance > stoppingDistance)
            {
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }

            if (Time.time >= nextFireTime)
            {
                Shoot(direction);
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot(Vector2 direction)
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("bulletPrefab не установлен в инспекторе!");
            return;
        }

        Vector3 spawnPosition = transform.position + new Vector3(0, bulletSpawnOffset, 0);
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        boolrealis bulletScript = bullet.GetComponent<boolrealis>();

        if (rb != null && bulletScript != null)
        {
            rb.velocity = direction * bulletScript.bulletSpeed;
        }
        else
        {
            if (rb == null)
                Debug.LogError("Rigidbody2D не найден на объекте пули!");
            if (bulletScript == null)
                Debug.LogError("Скрипт Bullet не найден на объекте пули!");
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