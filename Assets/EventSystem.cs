using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventSystem : MonoBehaviour
{
    public Text countTextField; // �ؽ�Ʈ �ʵ� UI
    public Image image; // �̹��� UI

    public Sprite image1; // ù ��° �̹���
    public Sprite image2; // �� ��° �̹���

    private int count; // count ����

    void Start()
    {
        count = 0;
        countTextField.text = count.ToString();

        // �ʱ� �̹��� ����
        image.sprite = image1;
    }

    void Update()
    {
        countTxt(); // count �� ������Ʈ

        // count�� ���� �̹����� ��ȯ��
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
