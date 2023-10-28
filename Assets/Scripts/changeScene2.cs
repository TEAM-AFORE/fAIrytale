using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene2 : MonoBehaviour
{

    public void SceneChange()
    {
        SceneManager.LoadScene("2_tutorial_1_room");
    }
}
