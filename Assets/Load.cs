using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene(8); // "GameScene"으로 씬을 로드합니다.
    }
}
