using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStateManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject enemyManagerPrefab;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public GameObject gameplayUI;
    public TextMeshProUGUI timerText;
    public GameObject playButton;
    public GameObject screen;

    private GameObject playerInstance;
    private GameObject enemyManagerInstance;

    private float survivalTime = 0f;
    private bool gameRunning = false;

    void Start()
    {

    }

    void Update()
    {
        if (gameRunning)
        {
            survivalTime += Time.deltaTime;

            if(timerText != null)
            {
                timerText.text = "Time: " + Mathf.FloorToInt(survivalTime).ToString() + "s";
            }
        }
    }
    public void StartGame()
    {
        if (playerPrefab != null)
        {
            playerInstance = Instantiate(playerPrefab, new Vector3(0f, 1f, 0f), Quaternion.identity);
        }

        if (enemyManagerPrefab != null)
        {
            enemyManagerInstance = Instantiate(enemyManagerPrefab, new Vector3(0f, 1f, 0f), Quaternion.identity);
        }

        survivalTime = 0f;
        gameRunning = true;

        if (timerText != null)
        {
            timerText.text = "Time: 0.0";
        }

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (playButton != null) playButton.SetActive(false);
        if (screen != null) screen.SetActive(false);
        if (gameplayUI != null) gameplayUI.SetActive(true);
    }

    public void EndGame()
    {
        if (!gameRunning) return;

        gameRunning = false;

        int score = Mathf.FloorToInt(survivalTime);
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }

        if (gameplayUI != null) gameplayUI.SetActive(false);
        if (playButton != null) playButton.SetActive(true);
        if (screen != null) screen.SetActive(true);
        if (gameOverPanel != null) gameOverPanel.SetActive(true);

        if (playerInstance != null) Destroy(playerInstance);
        if (enemyManagerInstance != null) Destroy(enemyManagerInstance);

        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        Debug.Log("Game Over! Time survived: " + score + " seconds");
    }
}
