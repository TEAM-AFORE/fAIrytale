using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad = "2_tutorial_4_livingroom"; // 전환할 씬의 이름

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            // 플레이어와 Door 간의 충돌이 발생하면 지정한 씬으로 전환
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
