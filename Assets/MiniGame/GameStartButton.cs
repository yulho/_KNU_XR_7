using System.Collections;
using UnityEngine;
using TMPro;

public class GameStartButton : MonoBehaviour
{
    public TMP_Text countdownText;
    public GameObject targetPrefab;
    public Transform targetSpawnArea;
    public ScoreManager scoreManager;
    public Canvas mainCanvas;

    private void Start()
    {
        mainCanvas.gameObject.SetActive(false); 
    }

    public void OnButtonPressed()
    {
        StartGame();
    }

    private void StartGame()
    {
        mainCanvas.gameObject.SetActive(true); 
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        countdownText.gameObject.SetActive(true);
        for (int i = 5; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        countdownText.gameObject.SetActive(false);
        SpawnTarget();
    }

    private void SpawnTarget()
    {
        Vector3 randomPosition = new Vector3(
            UnityEngine.Random.Range(targetSpawnArea.position.x - targetSpawnArea.localScale.x / 2, targetSpawnArea.position.x + targetSpawnArea.localScale.x / 2),
            UnityEngine.Random.Range(targetSpawnArea.position.y - targetSpawnArea.localScale.y / 2, targetSpawnArea.position.y + targetSpawnArea.localScale.y / 2),
            UnityEngine.Random.Range(targetSpawnArea.position.z - targetSpawnArea.localScale.z / 2, targetSpawnArea.position.z + targetSpawnArea.localScale.z / 2)
        );
        Instantiate(targetPrefab, randomPosition, Quaternion.identity);
    }
}
