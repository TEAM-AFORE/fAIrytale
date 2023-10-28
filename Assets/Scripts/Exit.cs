using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic; // Added this line for List<>
using UnityEngine.SceneManagement;
using System.Text;
using System;
using Newtonsoft.Json; // Json.NET ���ӽ����̽� �߰�

using Newtonsoft.Json.Linq;
using static ApiService;


public class Exit : MonoBehaviour
{
    public string targetSceneName;
    public Button changeSceneButton; // ��ư ����

    public string npc_name = "";
    void Start()
    {
        // ��ư Ŭ�� �� ChangeSceneOnClick �Լ� ȣ��
        changeSceneButton.onClick.AddListener(ChangeScene);
    }

    void ChangeScene()
    {
        // �� ��ȯ
        StartCoroutine(MakeRequest());
     
        SceneManager.LoadScene(targetSceneName);
    }



    IEnumerator MakeRequest()
    {
        //POST
        String sendUrl ="http://52.78.50.61/fairy-tale/user2npc/talkToNPC";
        String inputText = "exit";


        contentJsonData jsonData = new contentJsonData();

        jsonData.npc_name = npc_name.ToString();
        jsonData.content = inputText;

        string jsonStr = JsonUtility.ToJson(jsonData);

        Debug.Log(jsonStr);

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStr);
        UnityWebRequest webRequest = new UnityWebRequest(sendUrl, "POST");
        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
           webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + webRequest.error);
        }
        else
        {
            Debug.Log(webRequest.downloadHandler.text);
        }

        webRequest.Dispose();
    }

    [Serializable]
    class NPCJsonData
    {
        public string npc_name;
    }
    class contentJsonData
    {
        public string content;
        public string npc_name;
    }

}

