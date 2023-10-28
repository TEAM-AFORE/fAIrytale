using UnityEngine;
using UnityEngine.UI;

public class InputFieldHandler : MonoBehaviour
{
    public InputField inputField;

    private void Start()
    {
        // InputField 컴포넌트와 연결된 스크립트에서 해당 InputField를 설정하고
        // 이벤트 핸들러를 연결합니다.
        inputField.onEndEdit.AddListener(SubmitUserInput);
    }

    private void SubmitUserInput(string userInput)
    {
        // 사용자 입력 처리
        Debug.Log("사용자 입력: " + userInput);

        // 다음 동작 또는 대화 진행 로직을 추가할 수 있습니다.
    }
}
