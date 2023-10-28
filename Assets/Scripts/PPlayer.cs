using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPlayer : MonoBehaviour
{
    public int PPlayerDamage = 10;
    public float PAttackDelay = 1f; // ���� ������ �ð� (��)
    public float knockbackForce = 1000f; // �˹� ��

    private bool PIsAttacking = false; // ���� ���� ������ ���θ� ��Ÿ���� ����

    // Animator ������Ʈ ����
    private Animator animator;

    private void Start()
    {
        // Animator ������Ʈ ��������
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
            // Attack �ִϸ��̼� ���
            animator.SetTrigger("Attack");
            PIsAttacking = true; // ���� ���� ������ ����

            Pmonster.PTakeDamage(PPlayerDamage);

            // ���Ϳ��� �˹� ȿ�� �ֱ�
            Vector2 knockbackDirection = (Pmonster.transform.position - transform.position).normalized;
            Vector2 knockback = knockbackDirection * knockbackForce;

            // ������ Rigidbody2D ������Ʈ ��������
            Rigidbody2D monsterRigidbody = Pmonster.GetComponent<Rigidbody2D>();
            if (monsterRigidbody != null)
            {
                monsterRigidbody.AddForce(knockback, ForceMode2D.Impulse);
            }

            // ������ �ð� �Ŀ� ���� �Ϸ� ó���� ȣ��
            StartCoroutine(CompleteAttackAfterDelay(PAttackDelay));
        }
    }

    private IEnumerator CompleteAttackAfterDelay(float delay)
    {
        // ������ �ð���ŭ ���
        yield return new WaitForSeconds(delay);

        // ������ ���Ŀ� ���� �Ϸ� ó��
        PIsAttacking = false; // ���� �Ϸ�
        animator.SetTrigger("Idle"); // Attack �ִϸ��̼� ���� �� Idle ���·� ��ȯ
    }
}
