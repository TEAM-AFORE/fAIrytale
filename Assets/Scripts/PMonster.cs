using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class PMonster : MonoBehaviour
{
    public int PMonsterHealth = 100;
    public Slider monsterHealthSlider; // Slider UI 요소를 저장할 변수
    public string targetSceneName;
    public Text result;


    [System.Serializable]

    private class GameResultData
    {
        public bool game_result = true;
    }
    private void Start()
    {
        UpdateMonsterHealthUI(); // 시작할 때 체력 UI를 업데이트합니다.
    }

    public void PTakeDamage(int damage)
    {
        PMonsterHealth -= damage;
        Debug.Log("Monster Health: " + PMonsterHealth);

        UpdateMonsterHealthUI(); // 체력이 변경될 때마다 UI를 업데이트합니다.

        if (PMonsterHealth <= 0)
        {
            PDie();
        }
    }

    public void PDie()
    {
        Debug.Log("Monster died.");
        //Text 출력 : 성공

        result.text = "성공!";

        bool gameResult = true;  // Set your game result here
        GameResultData gameResultData = new GameResultData { game_result = gameResult };

        // Convert GameResultData to JSON
        string jsonInput = JsonUtility.ToJson(gameResultData);

        // Send the POST request
        StartCoroutine(SendPostRequest(jsonInput));


        Invoke("SwitchScene", 5f);

    }

    private void UpdateMonsterHealthUI()
    {
        if (monsterHealthSlider != null)
        {
            monsterHealthSlider.value = PMonsterHealth; // Slider UI에 체력 정보를 업데이트합니다.
        }
    }

    private IEnumerator SendPostRequest(string jsonInput)
    {
        string storyUrl = "http://52.78.50.61/fairy-tale/final/game";
        // Prepare the request
        UnityWebRequest webRequest = new UnityWebRequest(storyUrl, "POST");
        byte[] jsonBytes = new System.Text.UTF8Encoding().GetBytes(jsonInput);
        webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonBytes);
        webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        // Send the request
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
            webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Post Request Error: " + webRequest.error);
        }
        else
        {
            Debug.Log("Post Request Successful! Response: " + webRequest.downloadHandler.text);
        }

        webRequest.Dispose();
    }

    void SwitchScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }

}

