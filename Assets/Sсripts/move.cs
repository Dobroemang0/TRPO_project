using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform shootingPoint;
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;
    public int health = 3;
    public GameObject heartPrefab;

    private float nextFireTime = 0f;
    private static int enemiesKilled = 0;
    private float timer = 0f;
    public TMP_Text timerText;
    public TMP_Text scoreText;

    public GameObject objectToActivate;
    public GameObject objectToDeactivate;

    void Start()
    {
        UpdateHeartsDisplay();
        UpdateScoreDisplay();
    }

    void Update()
    {
        timer += Time.deltaTime;
        UpdateTimerDisplay();

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        transform.Translate(movement * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = shootingPoint.up * bulletSpeed;
        }
        else
        {
            Debug.LogError("Rigidbody2D не найден на объекте пули!");
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHeartsDisplay();

        if (health <= 0)
        {
            ToggleGameObject();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bool") || collision.gameObject.CompareTag("enemy"))
        {
            TakeDamage(1);
        }
    }

    public static void IncreaseScore()
    {
        enemiesKilled++;
        UpdateScoreDisplay();
    }

    public void UpdateHeartsDisplay()
    {
        GameObject[] existingHearts = GameObject.FindGameObjectsWithTag("Heart");
        foreach (GameObject heart in existingHearts)
        {
            Destroy(heart);
        }

        for (int i = 0; i < health; i++)
        {
            Vector3 heartPosition = new Vector3(2 + i * 2, 4.25f, 0);
            GameObject heart = Instantiate(heartPrefab, heartPosition, Quaternion.identity);
            heart.tag = "Heart";
        }
    }

    public void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public static void UpdateScoreDisplay()
    {
        PlayerMovement instance = FindObjectOfType<PlayerMovement>();
        if (instance != null && instance.scoreText != null)
        {
            instance.scoreText.text = "Enemies Killed: " + enemiesKilled;
        }
        else
        {
            Debug.LogError("Не удалось обновить счёт: компонент scoreText не найден.");
        }
    }

    public void ToggleGameObject()
    {
        if (objectToActivate != null && objectToDeactivate != null)
        {
            objectToActivate.SetActive(true);
            objectToDeactivate.SetActive(false);
        }
        else
        {
            Debug.LogError("Один из объектов равен null.");
        }
    }

    public static int GetEnemiesKilled()
    {
        return enemiesKilled;
    }

    public float GetElapsedTime()
    {
        return timer;
    }

    public static void ResetScore()
    {
        enemiesKilled = 0;
        UpdateScoreDisplay();
    }

    public void ResetTime()
    {
        timer = 0f;
        UpdateTimerDisplay();
    }
}