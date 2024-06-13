using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GunController : MonoBehaviour
{
    public XRController controller;
    private FireBullet fireBullet;

    private void Start()
    {
        fireBullet = GetComponent<FireBullet>();
    }

    private void Update()
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed) && triggerPressed)
        {
            fireBullet.Fire();
        }
    }
}
