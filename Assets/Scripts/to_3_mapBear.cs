using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class to_3_mapBear : MonoBehaviour
{
    void Start()
    {
        // 3초 후에 MoveToMapBear 함수를 호출합니다.
        Invoke("MoveToMapBear", 3f);
    }

    void MoveToMapBear()
    {
        // "3_mapBear" 씬으로 이동합니다.
        SceneManager.LoadScene("3_mapMouse");
    }
}
