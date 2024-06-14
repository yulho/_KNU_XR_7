using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlay : MonoBehaviour
{
    public GameObject CalenderUI;
    public GameObject OverlayUI;

    // Start is called before the first frame update
    void Start()
    {
        OverlayUI.SetActive(true);
        CalenderUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnButtonClick()
    {
        
        OverlayUI.SetActive(false);
        CalenderUI.SetActive(true);
    }
}
