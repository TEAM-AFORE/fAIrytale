using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager4 : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    //���� ������ �־����
    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    
    void GenerateData()
    {
        talkData.Add(0, new string[] { "DummyData", "test"});
        //int���� npc ID �ֱ�(�츮�� ���� 1�� ���̹Ƿ� 0���� ��)
        //���� ������ ��������Ƿ� string[]���� ���, chat gpt�� �޾ƿ���!

    }

    public string GetTalk(int id, int talkIndex)
    {
        //��� ��������
        return talkData[id][talkIndex];
        //�� ���� �� �����ͼ� ���� ��

    }
}
