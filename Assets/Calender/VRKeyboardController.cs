using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using System.IO;


public class VRKeyboardController : MonoBehaviour
{
    public GameObject vrKeyboard;  
    public TMP_InputField inputField;  

    void Start()
    {
        vrKeyboard.SetActive(false);  
    }
    public void OnInputFieldSelected()
    {
        vrKeyboard.SetActive(true);  
    }

    public void OnCloseKeyboard()
    {
        vrKeyboard.SetActive(false);  
    }

}

