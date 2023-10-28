using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class NewVideoPlayerController : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private bool hasStarted = false;

    [SerializeField]
    private string nextSceneName = "SceneName"; // 씬 전환을 위한 다음 씬의 이름을 지정하세요.

    [SerializeField]
    private float delayBeforeSceneTransition = 3f; // 비디오 재생 후 씬 전환 대기 시간 (초)

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoFinished;

        // 비디오 재생을 시작하기 전 일정 시간 동안 대기
        StartCoroutine(StartVideoAfterDelay(delayBeforeSceneTransition));
    }

    IEnumerator StartVideoAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 일정 시간 대기 후 비디오 재생 시작
        videoPlayer.Play();
        hasStarted = true;
    }

    void Update()
    {
        if (hasStarted && Input.GetKeyDown(KeyCode.Escape))
        {
            // ESC 키를 누르면 비디오 스킵하고 다음 씬으로 이동
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        // 동영상이 끝나면 호출되는 메서드
        SceneManager.LoadScene(nextSceneName);
    }
}
