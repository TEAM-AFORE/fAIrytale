using UnityEngine;
using UnityEngine.UI;

public class InputFieldHandler : MonoBehaviour
{
    public InputField inputField;

    private void Start()
    {
        // InputField ������Ʈ�� ����� ��ũ��Ʈ���� �ش� InputField�� �����ϰ�
        // �̺�Ʈ �ڵ鷯�� �����մϴ�.
        inputField.onEndEdit.AddListener(SubmitUserInput);
    }

    private void SubmitUserInput(string userInput)
    {
        // ����� �Է� ó��
        Debug.Log("����� �Է�: " + userInput);

        // ���� ���� �Ǵ� ��ȭ ���� ������ �߰��� �� �ֽ��ϴ�.
    }
}
