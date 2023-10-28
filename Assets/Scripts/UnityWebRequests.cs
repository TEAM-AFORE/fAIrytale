using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UnityWebRequests : MonoBehaviour
{
    private string apiUrl = "http://52.78.50.61/fairy-tale/npc2npc/interviewNPC";
    public Text resultTxt;
    public string targetSceneName;

    public string npc_name="";
    void Start()
    {
        StartCoroutine(interview_npc());
        Invoke("SwitchScene", 300.0f);
    }


    private IEnumerator interview_npc()
    {


        // Create JSON data
        JsonData jsonData = new JsonData();
        string jsonStr = JsonUtility.ToJson(jsonData);

        // Prepare the request
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonStr);
        UnityWebRequest webRequest = new UnityWebRequest(apiUrl, "POST");

        webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        // Send the request
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
            webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + webRequest.error);
        }
        else
        {
            Debug.Log("Response: " + webRequest.downloadHandler.text);

            // Parse the JSON response
            InterviewData jsonResponse = JsonUtility.FromJson<InterviewData>(webRequest.downloadHandler.text);

            // Extract the desired part starting with "∞ı¿Ã"
            string extractedPart = jsonResponse.dialogues;
            resultTxt.text = extractedPart;

           
        }

        webRequest.Dispose();
    }
    void SwitchScene()
    {
        // ¡ˆ¡§µ» æ¿¿∏∑Œ ¿¸»Ø
        SceneManager.LoadScene(targetSceneName);
    }
}
[System.Serializable]
class JsonData
{
    string npc_name ;
    bool pursuade_tf = true;     //º≥µÊ ø©∫Œ
}
[System.Serializable]
class InterviewData
{
    public string dialogues;
}
