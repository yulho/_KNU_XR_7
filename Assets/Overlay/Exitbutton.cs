using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exitbutton : MonoBehaviour
{
    public GameObject CalenderUI;
    public GameObject OverlayUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnButtonClick()
    {
        OverlayUI.SetActive(true);
        CalenderUI.SetActive(false);
    }
}
