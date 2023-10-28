using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic; // Added this line for List<>
using UnityEngine.SceneManagement;
using System.Text;
using System;
using Newtonsoft.Json; // Json.NET 네임스페이스 추가

using Newtonsoft.Json.Linq;
using static ApiService;

public class ApiService : MonoBehaviour
{

    public Text textmesh; // Legacy UI TextMesh 
    public InputField inputField; // Legacy UI InputField
    private string apiUrl = "http://52.78.50.61/fairy-tale";

    public Text count;
    public string npc_name ="";
    public string next_scence;

    private int talkcount = 0;
    private string inputText;

    void Start()
    {
        StartCoroutine(SendDataToServer());
    }

    IEnumerator SendDataToServer()
    {
        //NPC 생성
        String sendUrl = apiUrl + "/user2npc/start";

        NPCJsonData jsonData = new NPCJsonData();
        jsonData.npc_name = npc_name;

        string jsonStr = JsonUtility.ToJson(jsonData);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStr);

        UnityWebRequest webRequest = new UnityWebRequest(sendUrl, "POST");

        Debug.Log("input : "+jsonStr);

        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
            webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + webRequest.error);
            HandleRequestError();
        }
        else
        {
            Debug.Log(webRequest.downloadHandler.text);
        }

        webRequest.Dispose();
    }

    public void update()
    {
        if (talkcount >= 5 || inputField.text=="exit")
        {
            //Scence 바꾸기
            inputText = "exit";

            StartCoroutine(MakeRequest());
            SceneManager.LoadScene(next_scence);
        }
        else
        {
            talkcount++;
            inputText = inputField.text;
            count.text = talkcount + "/5";
            StartCoroutine(MakeRequest());
        }
    }


    IEnumerator MakeRequest()
    {
        //POST
        String sendUrl = apiUrl + "/user2npc/talkToNPC";
        

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
            HandleRequestError();
        }
        else
        {
            Debug.Log(webRequest.downloadHandler.text);
            HandleRequestSuccess(webRequest.downloadHandler.text);
        }

        webRequest.Dispose();
    }
    private IEnumerator HandleRequestError()
    {
        //get
        String sendUrl = apiUrl + "/user2npc/record";

        UnityWebRequest webRequest = UnityWebRequest.Get(sendUrl);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
            webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + webRequest.error);
        }
        else
        {
            Debug.Log("Response: " + webRequest.downloadHandler.text);
        }

        // Be sure to dispose of the UnityWebRequest object when done
        webRequest.Dispose();

    }

    private void HandleRequestSuccess(string responseText)
    {
        // JSON 형식의 문자열 파싱
        JObject data = JObject.Parse(responseText);

        // Extract content from the response
        string content = data["content"].ToString();

        // Display the content
        textmesh.text = content;

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
    
    [System.Serializable]
    public class MyJsonData
    {
        public List<List<OutputData>> data;
    }

    [System.Serializable]
    public class message
    {
        public string role;
        public string content;
    }

    [System.Serializable]
    public class OutputData
    {
        public int prompt_tokens;
        public int total_tokens;
        public string role;
        public string content;
        public List<message> message;
    }


}