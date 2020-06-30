using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomerWindow : MonoBehaviour
{
    public GameObject windowBackground;
    public Text name;
    public Text getMoney;
    public Text stamp;
    public Image charactreImage;
    public Text info;
    public Text unlockCondition;
    public Image[] itemImages;
    public Sprite nullImage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetWindowInfomation(string name, string info, string unlockCondition, string getMoney, string maxStamp, string currentStamp, Sprite[] itemImages, Image characterSprite, bool unlockState, Customer target)
    {
        OpenWindow();
        this.name.text = name;
        this.info.text = info;
        if (unlockState)
        {
            this.unlockCondition.text = "등장조건 : " + unlockCondition;
            this.getMoney.text = getMoney + "을(를) 수금하였습니다";
            this.stamp.text = currentStamp + " / " + maxStamp;
            for (int i = 0; i < 3; i++)
            {
                if (target.itemActive[i])
                {
                    this.itemImages[i].sprite = itemImages[i];
                }
                else
                {
                    this.itemImages[i].sprite = nullImage;
                }
            }
        }
        else
        {
            this.unlockCondition.text = "";
            this.getMoney.text = "";
            this.stamp.text = "";
            for (int i = 0; i < 3; i++)
            {
                this.itemImages[i].sprite = nullImage;
            }
        }
        this.charactreImage.sprite = characterSprite.sprite;
        this.charactreImage.color = characterSprite.color;
        this.charactreImage.GetComponent<RectTransform>().sizeDelta = characterSprite.GetComponent<RectTransform>().sizeDelta;
    }

    public void OpenWindow()
    {
        windowBackground.SetActive(true);
        gameObject.SetActive(true);
    }

    public void CloseWindow()
    {
        windowBackground.SetActive(false);
        gameObject.SetActive(false);
    }
}
