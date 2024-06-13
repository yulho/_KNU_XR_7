/*using System;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public float speed = 50f;
    public GameObject bulletPrefab;
    public Transform frontOfGun;

    public static event Action GunFired;

    public void Fire()
    {
        GetComponent<AudioSource>().Play();
        GameObject spawnedBullet = Instantiate(bulletPrefab, frontOfGun.position, frontOfGun.rotation);
        Rigidbody rb = spawnedBullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = speed * frontOfGun.forward;
        }
        Destroy(spawnedBullet, 5f);
        GunFired?.Invoke();
    }
}*/

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FireBullet : MonoBehaviour
{
    public float speed = 50f;
    public GameObject bulletObj;
    public Transform frontOfGun;

    public static event Action GunFired;
    public void Fire()
    {
        GetComponent<AudioSource>().Play();
        GameObject spawnedBullet = Instantiate(bulletObj, frontOfGun.position, frontOfGun.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * frontOfGun.forward;
        Destroy(spawnedBullet, 5f);
        GunFired?.Invoke();
    }


}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FireBullet : MonoBehaviour
{
    public float speed = 50f;
    public GameObject bulletObj;
    public Transform frontOfGun;
    public LayerMask buttonLayerMask; // 버튼 레이어 마스크

    public static event Action GunFired;

    public void Fire()
    {
        GetComponent<AudioSource>().Play();
        GameObject spawnedBullet = Instantiate(bulletObj, frontOfGun.position, frontOfGun.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * frontOfGun.forward;

        // Raycast로 버튼과의 충돌을 감지
        RaycastHit hit;
        if (Physics.Raycast(frontOfGun.position, frontOfGun.forward, out hit, Mathf.Infinity, buttonLayerMask))
        {
            if (hit.collider.CompareTag("StartButton"))
            {
                hit.collider.GetComponent<GameStartButton>().OnButtonPressed();
            }
            else if (hit.collider.CompareTag("ResetButton"))
            {
                hit.collider.GetComponent<ResetButton>().OnButtonPressed();
            }
        }

        Destroy(spawnedBullet, 5f);
        GunFired?.Invoke();
    }
}
