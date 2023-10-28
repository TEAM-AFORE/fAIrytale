using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventSystem : MonoBehaviour
{
    public Text countTextField; // 텍스트 필드 UI
    public Image image; // 이미지 UI

    public Sprite image1; // 첫 번째 이미지
    public Sprite image2; // 두 번째 이미지

    private int count; // count 변수

    void Start()
    {
        count = 0;
        countTextField.text = count.ToString();

        // 초기 이미지 설정
        image.sprite = image1;
    }

    void Update()
    {
        countTxt(); // count 값 업데이트

        // count에 따라 이미지가 변환됨
        UpdateImage();
    }

    private void countTxt()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            count++;
            countTextField.text = count.ToString();
        }
    }

    private void UpdateImage()
    {
        switch (count)
        {
            case 5:
                image.sprite = image2;
                break;
            default:
                image.sprite = image1;
                break;
        }
    }
}
