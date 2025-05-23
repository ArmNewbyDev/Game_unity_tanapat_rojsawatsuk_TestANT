using UnityEngine;
using UnityEngine.UI;

public class MainLogic : MonoBehaviour
{
    public int GetHP() => Mathf.Max(hp, 0);
    public float GetTimeRemaining() => timer;
    public int GetScore() => score;

    public int maxHP = 5;
    public float countdownTime = 120f;

    private float timer;
    private int score = 0;
    private int hp;

    private bool isPaused = false;
    private GameObject pauseUIInstance;

    void Start()
    {
        hp = maxHP;
        timer = countdownTime;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (!isPaused)
        {
            if (timer > 0)
            {
                timer -= Time.unscaledDeltaTime;
            }
            else
            {
                ShowGameOverUI();
            }
        }
    }

    public void AddScore() // Add Score for Player
    {
        score += 1;
        Debug.Log($"Score: {score}");
    }

    public void GetDamage() // -Health Point by 1 for Player 
    {
        hp -= 1;
        Debug.Log($"HP: {hp}");

        if (hp <= 0) // Must be  "<="  because if "<" less than 0 player will not die whem hp go to 0 it will die after -1 
        {
            ShowGameOverUI();
        }
    }

    private void TogglePause() // For Pause Game
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Time STOP! (ZA WORUDOOOO!)

            GameObject pauseUIPrefab = Resources.Load<GameObject>("UI/Pause");
            if (pauseUIPrefab != null)
            {
                pauseUIInstance = Instantiate(pauseUIPrefab); // Create UI prefab
            }
            else
            {
                Debug.LogWarning("ไม่พบ Pause UI ใน Resources/UI/Pause");
            }
        }
        else
        {
            Time.timeScale = 1f; // Time continue

            if (pauseUIInstance != null)
            {
                Destroy(pauseUIInstance); // Destroy pause UI if Unpause D:
            }
        }
    }

    private void ShowGameOverUI() // Show GameOver UI
    {
        // หยุดเกมเหมือนตอน Pause
        Time.timeScale = 0f;

        GameObject goUI = Resources.Load<GameObject>("UI/GameOver");
        if (goUI != null)
        {
            Instantiate(goUI); // Create UI
        }
        else
        {
            Debug.LogWarning("ไม่พบ GameOver UI ใน Resources/UI/GameOver");
        }

        enabled = false; // หยุด MainLogic
    }

}
