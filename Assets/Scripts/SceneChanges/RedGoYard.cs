using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedGoYard : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어가 Door 오브젝트에 닿으면 지정된 씬으로 이동합니다.
            SceneManager.LoadScene("2_tutorial_3_1_yard");
        }
    }
}
