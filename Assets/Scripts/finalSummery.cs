using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class finalSummery : MonoBehaviour
{
    public Text mouse;
    public Text wildcat;
    public Text bear;
    public Text final;

    //game_result에 따라 이미지 변경
    public Image image;
    public Sprite bad;

    private string apiUrl = "http://52.78.50.61/fairy-tale";
    private UnityWebRequest webRequest;

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

    private class GameResultData
    {
        public bool game_result = true;
    }
    private class Story
    {
        public string content;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SendGetRequest());
        StartCoroutine(SendStoryRequest());

        
    }

    private IEnumerator SendGetRequest()
    {
        //각 동물의 써머리
        string storyUrl = apiUrl + "/user2npc/summary";
        UnityWebRequest webRequest = UnityWebRequest.Get(storyUrl);
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

            mouse.text = "Mouse: " + responseData.pursuade[0].mouse;
            wildcat.text = "Wildcat: " + responseData.pursuade[1].wildcat;
            bear.text = "Bear: " + responseData.pursuade[2].bear;
           
        }

        // Be sure to dispose of the UnityWebRequest object when done
        
        webRequest.Dispose();
    }


    private IEnumerator SendStoryRequest()
    {
        string storyUrl = apiUrl + "/final/story";
        UnityWebRequest storyRequest = UnityWebRequest.Get(storyUrl);
        yield return storyRequest.SendWebRequest();

        if (storyRequest.result == UnityWebRequest.Result.ConnectionError ||
            storyRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Story Request Error: " + storyRequest.error);
        }
        else
        {
            string storyJson = storyRequest.downloadHandler.text;
            Story story = JsonUtility.FromJson<Story>(storyJson);

            final.text = "Final Story: " + story.content;
        }

        webRequest.Dispose();

    }


}
