using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth=100;
    public int currentHealth;
    public Text healthText; // Text UI ��Ҹ� ������ ����
    public Slider healthSlider; // Slider UI ��Ҹ� ������ ����
    public string targetSceneName;
    public Text result;

    public bool[] array;

    [System.Serializable]

    private class GameResultData
    {
        public bool game_result = true;
    }
    private class ResponseData
    {
        public PersuadeData[] pursuade;
    }
    private class PersuadeData
    {
        public string mouse;
        public string wildcat;
        public string bear;
        public bool pursuaded_tf;
    }

    private void Start()
    {
        SendGetRequest();        
        int trueCount = 0;
        foreach (bool value in array)
        {
            if (value)
                trueCount++;
        }

        if (trueCount == 3)
            maxHealth += 60;
        else if (trueCount == 2)
            maxHealth += 40;
        else if (trueCount == 1)
            maxHealth += 20;

        currentHealth = maxHealth;

        UpdateHealthUI(); // ������ �� ü�� UI�� ������Ʈ�մϴ�.
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current HP: " + currentHealth);

        UpdateHealthUI(); // ü���� ����� ������ UI�� ������Ʈ�մϴ�.

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died.");

        result.text = "����!";

        bool gameResult = false;  // Set your game result here
        GameResultData gameResultData = new GameResultData { game_result = gameResult };

        // Convert GameResultData to JSON
        string jsonInput = JsonUtility.ToJson(gameResultData);

        // Send the POST request
        StartCoroutine(SendPostRequest(jsonInput));


        Invoke("SwitchScene", 5f);

    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString(); // Text UI�� ü�� ������ ������Ʈ�մϴ�.
        }

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth; // Slider UI�� ü�� ������ ������Ʈ�մϴ�.
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



    private IEnumerator SendGetRequest()
    {
        //�� ������ ��Ӹ�
        string storyUrl = "http://52.78.50.61/fairy-tale/user2npc/summary";
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

            array[0] = responseData.pursuade[0].pursuaded_tf;
            array[1] = responseData.pursuade[1].pursuaded_tf;
            array[2] = responseData.pursuade[2].pursuaded_tf;
        }

        webRequest.Dispose();
    }




}
