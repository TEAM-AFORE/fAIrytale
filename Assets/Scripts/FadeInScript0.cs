using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using UnityEngine.UI;
using Unity.VisualScripting;

public class FadeInScript0 : MonoBehaviour
{
    public UnityEngine.UI.Image fade;
    float fades = 1.0f;
    float time = 0;

    void Update()
    {
        time += Time.deltaTime;
        if (fades > 0.0f && time >= 0.1f)
        {
            fades -= 0.1f;
            fade.color = new Color(0, 0, 0, fades);
            time = 0;
        }
        else if(fades <= 0.0f)
        {
            Thread.Sleep(3000);
            SceneManager.LoadScene(1);
        }
        
    }

}
