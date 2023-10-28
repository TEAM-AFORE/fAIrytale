using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MomDialogue : MonoBehaviour
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
            "안녕! 오늘 어땠어?",
            "날씨가 정말 좋아 보여. 밖에 나가 보려고 생각하니?",
            "아, 심부름 좀 해줄 수 있니?",
            "밖에 나가서 우체통 좀 확인해줄래?",
            "고마워.",
            "나중에 다시 얘기해. 행복한 하루 되길 바래.",
            "우체통 확인 좀 부탁할게."
        };

        playerController = FindObjectOfType<PlayerController3>();
        animator = GetComponent<Animator>();

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
