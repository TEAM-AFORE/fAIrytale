using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class changeScene3 : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("2_tutorial_3_2_AfterLetter");
    }
}
