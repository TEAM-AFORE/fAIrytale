using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCSceneChanger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bear"))
        {
            // Bear와 만났을 때 "Bear" 씬으로 이동
            SceneManager.LoadScene("4_talkWBear");
        }
        else if (other.CompareTag("WildCat"))
        {
            // WildCat과 만났을 때 "WildCat" 씬으로 이동
            SceneManager.LoadScene("4_talkWildCat");
        }
        else if (other.CompareTag("Mouse"))
        {
            // Mouse와 만났을 때 "Mouse" 씬으로 이동
            SceneManager.LoadScene("4_talkMouse");
        }

        else if (other.CompareTag("Mine"))
        {
            // Mouse와 만났을 때 "Mouse" 씬으로 이동
            SceneManager.LoadScene("5_Dungeon");
        }
    }
}
