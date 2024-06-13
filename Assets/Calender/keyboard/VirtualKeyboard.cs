using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VirtualKeyboard : MonoBehaviour
{
    // 현재 입력된 문자열을 저장할 변수
    private string currentInput = "";

    // 입력된 문자열을 표시할 InputField
    public TMP_InputField inputField;

    // 키 클릭 이벤트를 처리할 함수
    public void OnKeyClick(string key)
    {
        // 클릭된 키에 따라 다른 동작을 수행
        switch (key)
        {
            case "backspace":
                // Backspace 키를 클릭한 경우 마지막 문자를 제거
                if (currentInput.Length > 0)
                {
                    currentInput = currentInput.Substring(0, currentInput.Length - 1);
                }
                break;
            case "space":
                // Backspace 키를 클릭한 경우 마지막 문자를 제거
                currentInput += " ";
                break;
            case "reset":
                // Backspace 키를 클릭한 경우 마지막 문자를 제거
                currentInput = "";
                break;
            default:
                // 그 외의 키를 클릭한 경우, 해당 키를 현재 입력에 추가
                currentInput += key;
                break;
        }

        // InputField에 현재 입력된 문자열 업데이트
        inputField.text = currentInput;
    }
}

