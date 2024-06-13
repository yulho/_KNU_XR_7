using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurOver : MonoBehaviour
{
    public GameObject FurUI;
    // Start is called before the first frame update
    void Start()
    {
        FurUI.SetActive(false);
    }

    public void OnButtonClick()
    {
        FurUI.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
