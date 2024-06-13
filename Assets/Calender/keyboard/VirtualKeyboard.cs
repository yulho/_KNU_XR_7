using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VirtualKeyboard : MonoBehaviour
{
    // ���� �Էµ� ���ڿ��� ������ ����
    private string currentInput = "";

    // �Էµ� ���ڿ��� ǥ���� InputField
    public TMP_InputField inputField;

    // Ű Ŭ�� �̺�Ʈ�� ó���� �Լ�
    public void OnKeyClick(string key)
    {
        // Ŭ���� Ű�� ���� �ٸ� ������ ����
        switch (key)
        {
            case "backspace":
                // Backspace Ű�� Ŭ���� ��� ������ ���ڸ� ����
                if (currentInput.Length > 0)
                {
                    currentInput = currentInput.Substring(0, currentInput.Length - 1);
                }
                break;
            case "space":
                // Backspace Ű�� Ŭ���� ��� ������ ���ڸ� ����
                currentInput += " ";
                break;
            case "reset":
                // Backspace Ű�� Ŭ���� ��� ������ ���ڸ� ����
                currentInput = "";
                break;
            default:
                // �� ���� Ű�� Ŭ���� ���, �ش� Ű�� ���� �Է¿� �߰�
                currentInput += key;
                break;
        }

        // InputField�� ���� �Էµ� ���ڿ� ������Ʈ
        inputField.text = currentInput;
    }
}

