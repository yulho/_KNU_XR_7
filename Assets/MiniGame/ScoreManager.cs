using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText; 
    public TMP_Text endGameText; 
    public GameObject targetPrefab;
    public Transform targetSpawnArea;
    public Canvas mainCanvas; 

    private int score = 0;
    private int bulletsFired = 0;
    private int maxBullets = 20;

    private void OnEnable()
    {
        FireBullet.GunFired += OnGunFired;
    }

    private void OnDisable()
    {
        FireBullet.GunFired -= OnGunFired;
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void SpawnNewTarget()
    {
        Vector3 randomPosition = new Vector3(
            UnityEngine.Random.Range(targetSpawnArea.position.x - targetSpawnArea.localScale.x / 2, targetSpawnArea.position.x + targetSpawnArea.localScale.x / 2),
            UnityEngine.Random.Range(targetSpawnArea.position.y - targetSpawnArea.localScale.y / 2, targetSpawnArea.position.y + targetSpawnArea.localScale.y / 2),
            UnityEngine.Random.Range(targetSpawnArea.position.z - targetSpawnArea.localScale.z / 2, targetSpawnArea.position.z + targetSpawnArea.localScale.z / 2)
        );
        Instantiate(targetPrefab, randomPosition, Quaternion.identity);
    }

    private void OnGunFired()
    {
        bulletsFired++;
        if (bulletsFired >= maxBullets)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        endGameText.gameObject.SetActive(true);
        endGameText.text = "Game Over\nScore: " + score;
    }

    public void ResetGame()
    {
        score = 0;
        bulletsFired = 0;
        scoreText.text = "Score: " + score;
        endGameText.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(false); // �ʱ�ȭ �� Canvas ��Ȱ��ȭ
    }
}
