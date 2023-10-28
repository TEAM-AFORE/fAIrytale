using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MomDialogue_Talk : MonoBehaviour
{
    public GameObject momDialogueBox;
    public Text momDialogueText;
    public InputField userInputField;
    public Button sendButton;

    private List<string[]> momDialogueSets = new List<string[]>();
    private int currentDialogueSetIndex = 0;
    private int currentDialogueIndex = 0;
    private bool isDialogueActive = false;

    void Start()
    {
        momDialogueSets.Add(new string[]
        {
            "설득하는 방법을 알려줄게.",
            "설득은 이렇게 할 수 있어.",
            "밑에 글자를 입력하고, send 버튼을 눌러봐."
            //유저: "알겠어."
        });

        momDialogueSets.Add(new string[]
        {
            "그거야! 대화는 이렇게 하는 거란다.",
            "설득에 대해 알려줄게.",
            "설득이란, 상대편이 너의 이야기를 따르도록 여러 가지로 깨우쳐 말하는 걸 의미해.",
            "네가 왜 늑대를 찾아가야 하는지 말해봐.",
            "(할머니가 위험에 처했다는 걸 알려주자..)"
        });

        // 다른 대화 세트 추가
        momDialogueSets.Add(new string[]
        {
            "맞아... 할머니는 납치됐어.",
            "하지만 늑대를 물리치려는 건 너무 위험해. 우리는 더 현명한 방법을 찾아야 해.",
            "늑대와 싸우는 건 위험해.",
            "(지금, 나의 강인함을 어필해보면 설득을 하는 데 도움이 될거야.)",
            "(나는 늑대보다 강하다는 사실을 알려주자..)"
        });

        // 다른 대화 세트 추가
        momDialogueSets.Add(new string[]
        {
            "네 싸움 실력은 정말 대단하지만, 늑대와의 싸움은 다른 일이야.",
            "그들은 위험하고 예측하기 어렵다고 들었어. 우리는 더 안전한 방법을 찾아야 해.",
            "할머니를 도우려면 협력하고 지역 경찰에 도움을 청하자.",
            "(경찰 따위 믿을 수 없어...)"
        });

        // 다른 대화 세트 추가
        momDialogueSets.Add(new string[]
        {
            "무슨 소리를 하는 거니...",
            "(이런... 엄마의 신뢰도가 하락했다.)",
            "(엄마의 신뢰를 얻을 수 있는 말을 해야 해.)",
            "(다른 사람과 함께 할머니를 구하러 간다고 말해보자.)"
            //다른 사람들과 함께 할머니를 구하러 갈게.
        });

        // 다른 대화 세트 추가
        momDialogueSets.Add(new string[]
        {
            "다른 사람들과?",
            "...아이야, 다른 사람들과 함께 할머니를 구하러 가는 것은 늑대에 대한 위험을 줄일 수 있을 것 같아.",
            "하지만 그럼에도 불구하고 우리는 협력하고 조심스럽게 접근해야 해.",
            "다른 사람들과 함께 도움을 청할 수 있겠지만 네 안전을 위해서도 신중한 계획이 필요해.",
            "네게는 계획이 있니?",
            "(난 늑대만큼 강한 동물들을 동료로 만들거야..)"
        });

        // 다른 대화 세트 추가
        momDialogueSets.Add(new string[]
        {
            "네 생각은 이해하지만, 늑대와의 싸움은 매우 위험하고 예측하기 어려울 수 있어.",
            "늑대보다 강한 동물들을 동료로 만들어도 여전히 위험한 일이야.",
            "할머니를 구하는 데 우리는 늑대와의 싸움보다 협력과 지원을 찾는 것이 좋을 거라고 생각해.",
            "(이 마을의 행정 절차는 느려터졌어.)",
            "(도움을 기다리는 사이에 할머니는 늑대밥이 되어버릴 거야.)",
            "(법보다 주먹이 빠르다는 사실을 알려주자.)"
            //하지만 법보다 주먹이 더 빠른걸.
        });

        // 다른 대화 세트 추가
        momDialogueSets.Add(new string[]
        {
            "뭐?",
            ".......",
            "하하하!",
            "네 할머니도 젊으셨을 적에 늘 그런 말을 하셨는데.",
            "넌 할머니와 닮았어. 강하고 망설임이 없어.",
            "(잘하면 엄마를 설득할 수 있을 것 같아.)",
            "(나의 강인함을 다시 한 번 어필해보자.)",
            "(어렸을 적 할머니께 배웠던 무술에 대해 이야기해야겠어.)"
            //할머니께 배운 무술을 활용하여 늑대를 물리칠게.
        });

        // 다른 대화 세트 추가
        momDialogueSets.Add(new string[]
        {
            ".......",
            "빨간망토야, 네 모험에 대해 걱정이 많아. 나는 너의 안전을 생각하고 있어.",
            "하지만 네가 하는 말을 들어보니, 어쩌면 크게 걱정하지 않아도 되는 건지도 모르겠다.",
            "너의 모험을 허락할게",
            "(설득에 성공했다!!!)"
        });

        momDialogueBox.SetActive(false);
        userInputField.gameObject.SetActive(false);
        sendButton.gameObject.SetActive(false);
        sendButton.onClick.AddListener(SubmitUserInput);

        StartMomDialogue();
    }

    void Update()
    {
        if (isDialogueActive && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            ContinueMomDialogue();
        }
    }

    void StartMomDialogue()
    {
        isDialogueActive = true;
        momDialogueBox.SetActive(true);

        if (currentDialogueIndex < momDialogueSets[currentDialogueSetIndex].Length)
        {
            momDialogueText.text = momDialogueSets[currentDialogueSetIndex][currentDialogueIndex];
            Debug.Log("대화가 시작되었습니다.");
        }
        else
        {
            // 모든 대화가 끝났을 경우 대화 종료
            EndMomDialogue();
        }
    }

    void ContinueMomDialogue()
    {
        currentDialogueIndex++;

        if (currentDialogueIndex < momDialogueSets[currentDialogueSetIndex].Length)
        {
            momDialogueText.text = momDialogueSets[currentDialogueSetIndex][currentDialogueIndex];
            Debug.Log("다음 대화: " + momDialogueSets[currentDialogueSetIndex][currentDialogueIndex]);
        }
        else
        {
            // 모든 대화가 끝났을 경우 대화 종료
            EndMomDialogue();
        }
    }

    void SubmitUserInput()
    {
        // 사용자 입력 처리
        string userResponse = userInputField.text;
        Debug.Log("사용자 입력: " + userResponse);

        // 다음 대화 세트로 이동
        currentDialogueSetIndex++;

        if (currentDialogueSetIndex < momDialogueSets.Count)
        {
            currentDialogueIndex = 0;
            StartMomDialogue();
        }
        else
        {
            // 대화 종료 후 다음 씬으로 이동
            SceneManager.LoadScene("2_tutorial_6_summary"); // "NextSceneName"에 다음 씬의 이름을 넣어주세요.
        }

        // Send 버튼 숨기기
        momDialogueText.gameObject.SetActive(true);
        userInputField.gameObject.SetActive(false);
        sendButton.gameObject.SetActive(false);

        // Input Field 내용 비우기
        userInputField.text = "";
    }

    void EndMomDialogue()
    {
        isDialogueActive = false;
        userInputField.gameObject.SetActive(true);
        sendButton.gameObject.SetActive(true);
        Debug.Log("대화가 종료되었습니다.");
    }
}
