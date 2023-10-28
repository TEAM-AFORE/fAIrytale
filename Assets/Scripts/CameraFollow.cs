using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public Transform target; // 플레이어의 Transform을 여기에 할당해주세요
    public Vector3 offset = new Vector3(0f, 0f, -10f); // 카메라의 위치를 플레이어로부터 얼마나 떨어뜨릴지 설정합니다.

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
        }
    }
}
