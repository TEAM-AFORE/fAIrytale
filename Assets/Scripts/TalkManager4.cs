using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager4 : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    //더미 데이터 넣어놓음
    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    
    void GenerateData()
    {
        talkData.Add(0, new string[] { "DummyData", "test"});
        //int에는 npc ID 넣기(우리는 지금 1개 뿐이므로 0으로 함)
        //여러 문장이 들어있으므로 string[]으로 사용, chat gpt로 받아오기!

    }

    public string GetTalk(int id, int talkIndex)
    {
        //대사 가져오기
        return talkData[id][talkIndex];
        //한 문장 씩 가져와서 리턴 됨

    }
}
