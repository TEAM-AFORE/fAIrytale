using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class to_3_mapBear : MonoBehaviour
{
    void Start()
    {
        // 3�� �Ŀ� MoveToMapBear �Լ��� ȣ���մϴ�.
        Invoke("MoveToMapBear", 3f);
    }

    void MoveToMapBear()
    {
        // "3_mapBear" ������ �̵��մϴ�.
        SceneManager.LoadScene("3_mapMouse");
    }
}
