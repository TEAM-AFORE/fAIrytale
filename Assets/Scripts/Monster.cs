using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform target;
    public float speed = 50f;
    public int damage = 10;
    public float attackRange = 0.5f;
    public float attackDelay = 1f; // ���� ���� ������
    private bool canAttack = true; // ���� ���� ���θ� �����ϴ� ����

    private Animator animator;
    private SpriteRenderer spriteRenderer; // ������ SpriteRenderer ������Ʈ

    private bool isChasingPlayer = false; // �÷��̾ �߰� ������ ����

    private void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= attackRange && canAttack)
        {
            Attack();
        }
        else
        {
            // ���� ������ ����� ��
            if (isChasingPlayer)
            {
                // �̵� �ִϸ��̼� ����
                animator.SetBool("IsRunning", true);
                isChasingPlayer = false; // �÷��̾� �߰� �ߴ�
                //animator.SetTrigger("Idle"); // Idle �ִϸ��̼� ����
            }

            // �̵��� �� "Run" �ִϸ��̼��� ����
            animator.SetBool("IsRunning", true);
            MoveTowardsTarget();
        }

        // ������ ���� ����
        if (target.position.x < transform.position.x)
        {
            // �÷��̾ ���� ���ʿ� ������ ���͸� �������ϴ�.
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void MoveTowardsTarget()
    {
        if (target != null)
        {
            isChasingPlayer = true; // �÷��̾ �߰� ������ ����
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        canAttack = false;
        animator.SetBool("IsRunning", false); // �̵� �ִϸ��̼� ����
        animator.SetTrigger("Attack");

        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);

            Vector2 knockbackDirection = (target.position - transform.position).normalized;
            Vector2 knockbackForce = new Vector2(knockbackDirection.x, knockbackDirection.y) * 200f;

            Rigidbody2D playerRigidbody = target.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.AddForce(knockbackForce, ForceMode2D.Impulse);
            }

            Debug.Log("Monster attacked the player!");

            Invoke("EnableAttack", attackDelay);
        }
    }

    private void EnableAttack()
    {
        canAttack = true;
    }
}
