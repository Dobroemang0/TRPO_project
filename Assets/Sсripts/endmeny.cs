using UnityEngine;
using TMPro;

public class ScoreAndTimeDisplay : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public PlayerMovement player;
    public GameObject objectToActivate;
    public GameObject objectToDeactivate;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("PlayerMovement component not assigned in the inspector.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            int score = PlayerMovement.GetEnemiesKilled();
            float time = player.GetElapsedTime();

            scoreText.text = "Enemies Killed: " + score;
            timerText.text = "Time: " + FormatTime(time);

            if (Input.GetKeyDown(KeyCode.E))
            {
                PerformActions();
            }
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void PerformActions()
    {
        KillAllEnemies();

        if (player != null)
        {
            player.health = 3;
            player.UpdateHeartsDisplay();

            if (objectToActivate != null && objectToDeactivate != null)
            {
                objectToActivate.SetActive(true);
                objectToDeactivate.SetActive(false);
            }
            else
            {
                Debug.LogError("Object to activate or deactivate is not assigned in the inspector.");
            }
        }

        PlayerMovement.ResetScore();
        player.ResetTime();
    }

    private void KillAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}