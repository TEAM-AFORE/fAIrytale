using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController3 : MonoBehaviour
{
    public float jumpForce = 10000f;
    public float speed = 300.0f;
    private int jumpCount = 0;
    private bool isGrounded = false;
    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private AudioSource playerAudio;
    private SpriteRenderer spriteRenderer;
    public GameObject scanObject;

    // 대화 중인지 여부를 나타내는 변수
    private bool isDialogActive = false;

    public bool CanMove
    {
        get { return !isDialogActive; } // 대화 중이 아닐 때만 이동 가능
    }

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (CanMove) // 대화 중이 아닐 때만 이동 가능
        {
            if (Input.GetKey(KeyCode.Space) && jumpCount < 1 && isGrounded)
            {
                jumpCount++;
                Debug.Log("Jump Key Pressed");
                animator.SetTrigger("Jump");
                playerRigidbody.velocity = Vector2.zero;
                playerRigidbody.AddForce(new Vector2(0, jumpForce * 10));
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                animator.SetBool("IsRunning", true);
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                spriteRenderer.flipX = true;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                animator.SetBool("IsRunning", true);
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                spriteRenderer.flipX = false;
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }
        }
    }

    private void Talk()
    {
        playerRigidbody.velocity = Vector2.zero;
        SceneManager.LoadScene("4_talkWithNPC");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Dead")
        {
            playerRigidbody.velocity = Vector2.zero;
            Talk();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            //playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
            OnJumpAnimationEnd();
        }
    }

    // 애니메이션의 이벤트로부터 호출되는 함수
    public void OnJumpAnimationEnd()
    {
        animator.SetTrigger("Idle"); // Idle 애니메이션으로 전환
    }

    // 대화 중 여부를 설정하는 함수
    public void SetDialogActive(bool active)
    {
        isDialogActive = active;
    }


}
