using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RedLetterDialogue : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText; // Legacy Text로 변경

    public string[] redDialogueLines;

    private int currentDialogueIndex = 0;
    private bool isDialogueActive = false;

    // PlayerController3 스크립트를 참조하기 위한 변수
    private PlayerController3 playerController;
    private bool hasTalkedToPostBox = false;

    void Start()
    {
        dialogueBox.SetActive(false);

        redDialogueLines = new string[]
        {
            "앞에 바로 우체통이 있어.",
            "가까이 가서 편지를 읽어보자."
        };

        // PlayerController3 스크립트를 찾아 참조합니다.
        playerController = FindObjectOfType<PlayerController3>();

        // 씬이 시작될 때 대화를 시작합니다.
        StartRedDialogue();
    }

    void Update()
    {
        if (isDialogueActive)
        {
            if (currentDialogueIndex < redDialogueLines.Length)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ContinueRedDialogue();
                }
            }
            else if (!hasTalkedToPostBox)
            {
                hasTalkedToPostBox = true;
                Invoke("ChangeSceneAfterDelay", 3f); // 3초 후 씬 변경
            }
        }
    }

    void StartRedDialogue()
    {
        isDialogueActive = true;
        dialogueBox.SetActive(true);
        dialogueText.text = redDialogueLines[currentDialogueIndex];
        Debug.Log("대화가 시작되었습니다.");

        // 대화 시작 시 PlayerController3의 움직임을 제한
        if (playerController != null)
        {
            playerController.SetDialogActive(true);
        }
    }

    void ContinueRedDialogue()
    {
        if (currentDialogueIndex < redDialogueLines.Length - 1)
        {
            currentDialogueIndex++;
            dialogueText.text = redDialogueLines[currentDialogueIndex];
            Debug.Log("다음 대화: " + redDialogueLines[currentDialogueIndex]);
        }
        else
        {
            EndRedDialogue();
        }
    }

    void EndRedDialogue()
    {
        isDialogueActive = false;
        dialogueBox.SetActive(false);
        currentDialogueIndex = 0;
        Debug.Log("대화가 종료되었습니다.");

        // 대화 종료 시 PlayerController3의 움직임을 다시 활성화
        if (playerController != null)
        {
            playerController.SetDialogActive(false);
        }

        // 대화 종료 후 Text의 Visible을 false로 설정
        dialogueText.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PostBox"))
        {
            // PostBox에 닿으면 대화 시작
            if (!isDialogueActive)
            {
                StartRedDialogue();
            }
        }
        else if (other.CompareTag("Door"))
        {
            // Door에 닿으면 9번째 씬으로 이동
            SceneManager.LoadScene(9);
        }
    }

    void ChangeSceneAfterDelay()
    {
        // 3초 후 씬 변경
        SceneManager.LoadScene(9);
    }
}
