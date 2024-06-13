using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ScoreManager scoreManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("10P") || other.CompareTag("20P") || other.CompareTag("30P") || other.CompareTag("40P") || other.CompareTag("50P"))
        {
            int score = int.Parse(other.tag.Replace("P", ""));
            scoreManager.AddScore(score);
            Destroy(other.gameObject);
            scoreManager.SpawnNewTarget();
        }
        else
        {
            
        }

        Destroy(gameObject);
    }
}
