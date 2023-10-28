using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostBoxInteraction : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PostBox"))
        {
            // 플레이어와 접촉하면 "2_intro" 씬으로 이동
            SceneManager.LoadScene("2_intro");
        }
    }
}
