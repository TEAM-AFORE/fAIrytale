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
        // ��ٸ��� �ö󰡴� ���� ó��
        if (isClimbing)
        {
            float verticalInput = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, verticalInput * climbSpeed);

            // ��ٸ��� �ö󰡸鼭 ������ �����ϵ��� ó��
            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isClimbing = false;
            }
        }
        else
        {
            // ���� ������ ���θ� �˻��Ͽ� isGrounded�� ����
            isGrounded = Physics2D.OverlapCircle(transform.position, 0.2f, groundLayer);
        }
    }

    private void FixedUpdate()
    {
        // �߷� ���� ����
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
        // ��ٸ��� ������ ��ٸ��� Ÿ���� ����
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;
            isOnUpperFloor = false;
        }

        // ������ ������ �������� �̵� �����ϵ��� ����
        if (other.CompareTag("UpperFloor"))
        {
            isOnUpperFloor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // ��ٸ��� ������ ��ٸ� Ÿ�� ���� ����
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
        }

        // �������� ������ ���� �̵� ���� ����
        if (other.CompareTag("UpperFloor"))
        {
            isOnUpperFloor = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // �������� �̵� ���� ��, �������� �浹�� �����Ͽ� ��� �����ϵ��� ����
        if (isOnUpperFloor && collision.gameObject.CompareTag("UpperFloor"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // �������� �浹 ���� ����
        if (isOnUpperFloor && collision.gameObject.CompareTag("UpperFloor"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), false);
        }
    }
}