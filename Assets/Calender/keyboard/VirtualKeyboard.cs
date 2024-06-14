using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VirtualKeyboard : MonoBehaviour
{
    
    private string currentInput = "";
    
    public TMP_InputField inputField;

    
    public void OnKeyClick(string key)
    {
        switch (key)
        {
            case "backspace":
                if (currentInput.Length > 0)
                {
                    currentInput = currentInput.Substring(0, currentInput.Length - 1);
                }
                break;
            case "space":
                currentInput += " ";
                break;
            case "reset":
                currentInput = "";
                break;
            default:
                currentInput += key;
                break;
        }

        // InputField에 현재 입력된 문자열 업데이트
        inputField.text = currentInput;
    }
}

