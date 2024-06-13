using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            int points = 0;
            switch (collision.contacts[0].thisCollider.tag)
            {
                case "10P":
                    points = 10;
                    break;
                case "20P":
                    points = 20;
                    break;
                case "30P":
                    points = 30;
                    break;
                case "40P":
                    points = 40;
                    break;
                case "50P":
                    points = 50;
                    break;
                default:
                    points = 0;
                    break;
            }

            GameManager.Instance.AddScore(points); // ���� �߰�
            GameManager.Instance.SpawnTarget(); // ���ο� Ÿ�� ����
            Destroy(gameObject); // Ÿ�� ����
            Destroy(collision.gameObject); // �Ѿ� ����
        }
    }
}
