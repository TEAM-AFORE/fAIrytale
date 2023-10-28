using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RedLetterDialogue : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText; // Legacy Text�� ����

    public string[] redDialogueLines;

    private int currentDialogueIndex = 0;
    private bool isDialogueActive = false;

    // PlayerController3 ��ũ��Ʈ�� �����ϱ� ���� ����
    private PlayerController3 playerController;
    private bool hasTalkedToPostBox = false;

    void Start()
    {
        dialogueBox.SetActive(false);

        redDialogueLines = new string[]
        {
            "�տ� �ٷ� ��ü���� �־�.",
            "������ ���� ������ �о��."
        };

        // PlayerController3 ��ũ��Ʈ�� ã�� �����մϴ�.
        playerController = FindObjectOfType<PlayerController3>();

        // ���� ���۵� �� ��ȭ�� �����մϴ�.
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
                Invoke("ChangeSceneAfterDelay", 3f); // 3�� �� �� ����
            }
        }
    }

    void StartRedDialogue()
    {
        isDialogueActive = true;
        dialogueBox.SetActive(true);
        dialogueText.text = redDialogueLines[currentDialogueIndex];
        Debug.Log("��ȭ�� ���۵Ǿ����ϴ�.");

        // ��ȭ ���� �� PlayerController3�� �������� ����
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
            Debug.Log("���� ��ȭ: " + redDialogueLines[currentDialogueIndex]);
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
        Debug.Log("��ȭ�� ����Ǿ����ϴ�.");

        // ��ȭ ���� �� PlayerController3�� �������� �ٽ� Ȱ��ȭ
        if (playerController != null)
        {
            playerController.SetDialogActive(false);
        }

        // ��ȭ ���� �� Text�� Visible�� false�� ����
        dialogueText.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PostBox"))
        {
            // PostBox�� ������ ��ȭ ����
            if (!isDialogueActive)
            {
                StartRedDialogue();
            }
        }
        else if (other.CompareTag("Door"))
        {
            // Door�� ������ 9��° ������ �̵�
            SceneManager.LoadScene(9);
        }
    }

    void ChangeSceneAfterDelay()
    {
        // 3�� �� �� ����
        SceneManager.LoadScene(9);
    }
}
