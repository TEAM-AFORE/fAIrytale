using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LadderClimb : MonoBehaviour
{
    public float climbSpeed = 180f;
    public float jumpForce = 5f;
    public LayerMask groundLayer;
    public LayerMask ladderLayer;

    private bool isClimbing = false;
    private bool isGrounded = false;
    private bool isOnUpperFloor = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 사다리를 올라가는 동작 처리
        if (isClimbing)
        {
            float verticalInput = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, verticalInput * climbSpeed);

            // 사다리를 올라가면서 점프도 가능하도록 처리
            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isClimbing = false;
            }
        }
        else
        {
            // 땅에 닿은지 여부를 검사하여 isGrounded를 갱신
            isGrounded = Physics2D.OverlapCircle(transform.position, 0.2f, groundLayer);
        }
    }

    private void FixedUpdate()
    {
        // 중력 적용 해제
        if (isClimbing)
        {
            rb.gravityScale = 0f;
        }
        else
        {
            rb.gravityScale = 40f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 사다리에 닿으면 사다리를 타도록 설정
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;
            isOnUpperFloor = false;
        }

        // 윗층에 닿으면 윗층으로 이동 가능하도록 설정
        if (other.CompareTag("UpperFloor"))
        {
            isOnUpperFloor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 사다리를 떠나면 사다리 타기 상태 해제
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
        }

        // 윗층에서 떠나면 윗층 이동 상태 해제
        if (other.CompareTag("UpperFloor"))
        {
            isOnUpperFloor = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 윗층으로 이동 중일 때, 윗층과의 충돌을 무시하여 통과 가능하도록 설정
        if (isOnUpperFloor && collision.gameObject.CompareTag("UpperFloor"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 윗층과의 충돌 무시 해제
        if (isOnUpperFloor && collision.gameObject.CompareTag("UpperFloor"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), false);
        }
    }
}