using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 싱글턴 인스턴스

    public GameObject gun;
    public Transform gunInitialPosition;
    public TMP_Text scoreText;
    public TMP_Text countdownText;
    public TMP_Text endGameText;
    public GameObject gameCanvas;
    public GameObject startButton;
    public GameObject resetButton;
    public GameObject targetPrefab;
    public Transform targetSpawnArea;

    private int score = 0;
    private bool gameStarted = false;
    private int bulletsFired = 0;
    private int maxBullets = 20;
    private GameObject currentTarget;

    private void Awake()
    {
        // 싱글턴 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeGame();
    }

    // OnClick 메서드 추가
    public void OnButtonClick(string buttonName)
    {
        if (buttonName == "StartButton")
        {
            OnStartButtonClick();
        }
        else if (buttonName == "ResetButton")
        {
            OnResetButtonClick();
        }
    }

    public void OnStartButtonClick()
    {
        StartGame();
    }

    public void OnResetButtonClick()
    {
        ResetGame();
    }

    public void StartGame()
    {
        UnityEngine.Debug.Log("StartGame called");
        gameStarted = true;
        startButton.SetActive(false);
        resetButton.SetActive(true);
        countdownText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        bulletsFired = 0;
        StartCoroutine(StartCountdown());
    }

    public void ResetGame()
    {
        UnityEngine.Debug.Log("ResetGame called");
        InitializeGame();
        gun.transform.position = gunInitialPosition.position;
        gun.transform.rotation = gunInitialPosition.rotation;
    }

    private void InitializeGame()
    {
        UnityEngine.Debug.Log("InitializeGame called");
        gameStarted = false;
        score = 0;
        bulletsFired = 0;
        scoreText.text = "Score: 0";
        countdownText.text = "";
        endGameText.gameObject.SetActive(false);
        gameCanvas.SetActive(true);
        startButton.SetActive(true);
        resetButton.SetActive(false);
        DestroyCurrentTarget();
    }

    private IEnumerator StartCountdown()
    {
        UnityEngine.Debug.Log("StartCountdown started");
        for (int i = 5; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        countdownText.gameObject.SetActive(false);
        SpawnTarget();
    }

    public void SpawnTarget()
    {
        UnityEngine.Debug.Log("SpawnTarget called");
        if (currentTarget != null)
        {
            Destroy(currentTarget);
        }

        Vector3 randomPosition = new Vector3(
            UnityEngine.Random.Range(targetSpawnArea.position.x - targetSpawnArea.localScale.x / 2, targetSpawnArea.position.x + targetSpawnArea.localScale.x / 2),
            UnityEngine.Random.Range(targetSpawnArea.position.y - targetSpawnArea.localScale.y / 2, targetSpawnArea.position.y + targetSpawnArea.localScale.y / 2),
            UnityEngine.Random.Range(targetSpawnArea.position.z - targetSpawnArea.localScale.z / 2, targetSpawnArea.position.z + targetSpawnArea.localScale.z / 2)
        );
        currentTarget = Instantiate(targetPrefab, randomPosition, Quaternion.identity);
    }

    private void DestroyCurrentTarget()
    {
        UnityEngine.Debug.Log("DestroyCurrentTarget called");
        if (currentTarget != null)
        {
            Destroy(currentTarget);
        }
    }

    public void AddScore(int points)
    {
        if (gameStarted)
        {
            score += points;
            scoreText.text = "Score: " + score;
            bulletsFired++;
            if (bulletsFired >= maxBullets)
            {
                EndGame();
            }
        }
    }

    public void EndGame()
    {
        UnityEngine.Debug.Log("EndGame called");
        gameStarted = false;
        endGameText.text = "Game Over\nScore: " + score;
        endGameText.gameObject.SetActive(true);
        DestroyCurrentTarget();
    }
}
