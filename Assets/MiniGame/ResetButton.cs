using UnityEngine;
using TMPro;

public class ResetButton : MonoBehaviour
{
    public ScoreManager scoreManager;
    public Canvas mainCanvas;
    public TMP_Text countdownText;
    public TMP_Text scoreText;
    public Transform gunInitialPosition; 
    public GameObject gun; 

    public void OnButtonPressed()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        scoreManager.ResetGame();
        mainCanvas.gameObject.SetActive(false); 
        countdownText.text = ""; 
        scoreText.text = "Score: 0"; 

        
        gun.transform.position = gunInitialPosition.position;
        gun.transform.rotation = gunInitialPosition.rotation;
    }
}
