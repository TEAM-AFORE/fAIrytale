using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class summery_connect : MonoBehaviour
{
    public Text pursuaded;
    public Text summery;
    public string npc_name;
    public Image good;
    public Sprite IMG_8434;
    public string targetSceneName;
    private string apiUrl = "http://52.78.50.61/fairy-tale/user2npc/summary";
    
    [System.Serializable]
    private class PersuadeData
    {
        public string mouse;
        public string wildcat;
        public string bear;
        public bool pursuaded_tf;
    }

    [System.Serializable]
    private class ResponseData
    {
        public PersuadeData[] pursuade;
    }
    void Start()
    {
        StartCoroutine(SendGetRequest());
        Invoke("SwitchScene", 5f);
    }

    private IEnumerator SendGetRequest()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
            webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + webRequest.error);
        }
        else
        {
            string jsonResponse = webRequest.downloadHandler.text;
            Debug.Log("Response: " + jsonResponse);

            // Parse the JSON response
            ResponseData responseData = JsonUtility.FromJson<ResponseData>(jsonResponse);
            // Access the parsed data

            foreach (var item in responseData.pursuade)
            {
                Debug.Log("Animal: " + item.mouse + ", Persuaded: " + item.pursuaded_tf);
            }

            if (npc_name == "mouse")
            {
                if (responseData.pursuade[0].pursuaded_tf)
                {
                    pursuaded.text = "성공!";

                }
                else
                {
                    pursuaded.text = "실패!";
                    good.sprite = IMG_8434;
                }
                summery.text = "Mouse: " + responseData.pursuade[0].mouse;
                
            }
            else if (npc_name == "wildcat")
            {
                if (responseData.pursuade[1].pursuaded_tf)
                {
                    pursuaded.text = "성공!";
                }
                else
                {
                    pursuaded.text = "실패!";
                    good.sprite = IMG_8434;
                }
                summery.text = "Wildcat: " + responseData.pursuade[1].wildcat;
            }
            else if (npc_name == "bear")
            {
                if (responseData.pursuade[2].pursuaded_tf)
                {
                    pursuaded.text = "성공!";
                }
                else
                {
                    pursuaded.text = "실패!";

                    good.sprite = IMG_8434;
                }
                summery.text = "Bear: " + responseData.pursuade[2].bear;
            }
            else
            {
                pursuaded.text = "에러!";
                summery.text = "NPC not found.";
            }
        }

        // Be sure to dispose of the UnityWebRequest object when done

        webRequest.Dispose();
    }
    void SwitchScene()
    {
        // 지정된 씬으로 전환
        SceneManager.LoadScene(targetSceneName);
    }
}
