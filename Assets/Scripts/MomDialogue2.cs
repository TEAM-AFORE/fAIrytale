using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MomDialogue2 : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private bool isDialogueActive = false;
    private bool hasTalkedToMom = false;

    public string[] momDialogueLines;

    public GameObject momDialogueBox;
    public Text momDialogueText;
    private int currentDialogueIndex = 0;

    private PlayerController3 playerController;
    private Animator animator;

    void Start()
    {
        momDialogueBox.SetActive(false);
        momDialogueLines = new string[]
        {
            "고마워!",
            "오늘은 무슨 편지가 왔니?",
            "...","...",
            "늑대에게서 결투장을 받았다고?",
            ".......",
            "결국은 이런 일이 벌어졌구나.",
            "아아.......",
            ".......",
            "뭐?",
            "할머니를 구하고 싶다니.",
            "안 돼! 늑대는 너무 위험한 동물이야. 네가 다칠 수도 있어.",
            "......",
            "안 된다니까.",
            "정 가고 싶다면, 한 번 나를 설득해 봐."
        };

        playerController = FindObjectOfType<PlayerController3>();
        animator = GetComponent<Animator>(); // Animator 컴포넌트 추가

        momDialogueText.enabled = false;
    }

    void Update()
    {
        if (isDialogueActive)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                ContinueMomDialogue();
            }
        }
        else
        {
            if (isPlayerInRange && !hasTalkedToMom)
            {
                StartMomDialogue();
            }
        }
    }

    void StartMomDialogue()
    {
        isDialogueActive = true;
        momDialogueBox.SetActive(true);
        momDialogueText.enabled = true;

        if (!hasTalkedToMom)
        {
            currentDialogueIndex = 0;
        }
        else
        {
            currentDialogueIndex = momDialogueLines.Length - 1;
        }

        momDialogueText.text = momDialogueLines[currentDialogueIndex];
        Debug.Log("대화가 시작되었습니다.");

        playerController.SetDialogActive(true);

        // Stop the player's "Run" animation
        animator.SetBool("IsRunning", false);
    }

    void ContinueMomDialogue()
    {
        if (currentDialogueIndex < momDialogueLines.Length - 1)
        {
            currentDialogueIndex++;
            momDialogueText.text = momDialogueLines[currentDialogueIndex];
            Debug.Log("다음 대화: " + momDialogueLines[currentDialogueIndex]);
        }
        else
        {
            EndMomDialogue();
        }
    }

    void EndMomDialogue()
    {
        isDialogueActive = false;
        momDialogueBox.SetActive(false);
        Debug.Log("대화가 종료되었습니다.");

        momDialogueText.enabled = false;

        playerController.SetDialogActive(false);

        // Stop the player's "Run" animation
        animator.SetBool("IsRunning", false);

        hasTalkedToMom = true;

        // 마지막 대화가 끝난 후 5초 뒤에 8번째 씬으로 전환
        Invoke("LoadScene13", 2f);
    }

    void LoadScene13()
    {
        // "Scene13"은 전환할 씬의 이름입니다. 필요에 따라 수정하세요.
        SceneManager.LoadScene("2_tutorial_5_talk");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mom"))
        {
            isPlayerInRange = true;
            Debug.Log("플레이어와 닿았습니다.");

            // Stop the player's "Run" animation
            animator.SetBool("IsRunning", false);
        }
    }
}
