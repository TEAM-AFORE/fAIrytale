using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPlayer : MonoBehaviour
{
    public int PPlayerDamage = 10;
    public float PAttackDelay = 1f; // 공격 딜레이 시간 (초)
    public float knockbackForce = 1000f; // 넉백 힘

    private bool PIsAttacking = false; // 현재 공격 중인지 여부를 나타내는 변수

    // Animator 컴포넌트 참조
    private Animator animator;

    private void Start()
    {
        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !PIsAttacking)
        {
            PAttackMonster();
        }
    }

    public void PAttackMonster()
    {
        PMonster Pmonster = FindObjectOfType<PMonster>();
        if (Pmonster != null)
        {
            // Attack 애니메이션 재생
            animator.SetTrigger("Attack");
            PIsAttacking = true; // 현재 공격 중으로 설정

            Pmonster.PTakeDamage(PPlayerDamage);

            // 몬스터에게 넉백 효과 주기
            Vector2 knockbackDirection = (Pmonster.transform.position - transform.position).normalized;
            Vector2 knockback = knockbackDirection * knockbackForce;

            // 몬스터의 Rigidbody2D 컴포넌트 가져오기
            Rigidbody2D monsterRigidbody = Pmonster.GetComponent<Rigidbody2D>();
            if (monsterRigidbody != null)
            {
                monsterRigidbody.AddForce(knockback, ForceMode2D.Impulse);
            }

            // 딜레이 시간 후에 공격 완료 처리를 호출
            StartCoroutine(CompleteAttackAfterDelay(PAttackDelay));
        }
    }

    private IEnumerator CompleteAttackAfterDelay(float delay)
    {
        // 딜레이 시간만큼 대기
        yield return new WaitForSeconds(delay);

        // 딜레이 이후에 공격 완료 처리
        PIsAttacking = false; // 공격 완료
        animator.SetTrigger("Idle"); // Attack 애니메이션 종료 후 Idle 상태로 전환
    }
}
