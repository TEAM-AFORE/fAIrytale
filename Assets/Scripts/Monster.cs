using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform target;
    public float speed = 50f;
    public int damage = 10;
    public float attackRange = 0.5f;
    public float attackDelay = 1f; // 몬스터 공격 딜레이
    private bool canAttack = true; // 공격 가능 여부를 추적하는 변수

    private Animator animator;
    private SpriteRenderer spriteRenderer; // 몬스터의 SpriteRenderer 컴포넌트

    private bool isChasingPlayer = false; // 플레이어를 추격 중인지 여부

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
            // 공격 범위를 벗어났을 때
            if (isChasingPlayer)
            {
                // 이동 애니메이션 종료
                animator.SetBool("IsRunning", true);
                isChasingPlayer = false; // 플레이어 추격 중단
                //animator.SetTrigger("Idle"); // Idle 애니메이션 실행
            }

            // 이동할 때 "Run" 애니메이션을 실행
            animator.SetBool("IsRunning", true);
            MoveTowardsTarget();
        }

        // 몬스터의 방향 설정
        if (target.position.x < transform.position.x)
        {
            // 플레이어가 몬스터 왼쪽에 있으면 몬스터를 뒤집습니다.
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
            isChasingPlayer = true; // 플레이어를 추격 중임을 설정
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        canAttack = false;
        animator.SetBool("IsRunning", false); // 이동 애니메이션 종료
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
