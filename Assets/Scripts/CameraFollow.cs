using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public Transform target; // �÷��̾��� Transform�� ���⿡ �Ҵ����ּ���
    public Vector3 offset = new Vector3(0f, 0f, -10f); // ī�޶��� ��ġ�� �÷��̾�κ��� �󸶳� ����߸��� �����մϴ�.

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
        }
    }
}
