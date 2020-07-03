﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerInfo : MonoBehaviour
{
    public bool isUnlock = false;
    public string name;
    public string info;
    public string unlockCondition;
    public int getMoney;
    public int maxStamp;
    public int currentStamp;
    public Sprite[] itemImages = new Sprite[3];
    public CustomerWindow customerWindow;
    public Customer targetCustomer;

    [SerializeField]
    private Image mainImage;
    [SerializeField]
    private Text nameText;

    // Start is called before the first frame update
    void Start()
    {
        isUnlock = targetCustomer.unlock;
        getMoney = targetCustomer.money;

        GameMng.Instance.SaveGame();
        if (isUnlock)
        {
            nameText.text = name;
            mainImage.color = Color.white;
        }
        else
        {
            nameText.text = "???";
            mainImage.color = Color.black;
        }

        for (int i = 0; i < 3; i++)
        {
            itemImages[i] = targetCustomer.items[i].GetComponent<SpriteRenderer>().sprite;
            Debug.Log("Penguin");
        }
        for (int i = 0; i < 3; i++)
        {
            itemImages[i] = targetCustomer.items[i].GetComponent<SpriteRenderer>().sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        isUnlock = targetCustomer.unlock;
        getMoney = targetCustomer.money;
        currentStamp = targetCustomer.stamp;
    }

    public void ButtonClick()
    {
        Debug.Log(gameObject.name + "Click");
        if (isUnlock)
        {
            customerWindow.SetWindowInfomation(
                nameText.text,
                info,
                unlockCondition,
                getMoney.ToString(),
                maxStamp.ToString(),
                currentStamp.ToString(),
                itemImages,
                mainImage,
                isUnlock,
                targetCustomer);
        }
        else
        {
            customerWindow.SetWindowInfomation(
                nameText.text,
                "???",
                "",
                "",
                "",
                "",
                null,
                mainImage,
                isUnlock,
                targetCustomer);
        }
    }

    public void Unlock()
    {
        isUnlock = true;
        nameText.text = name;
        mainImage.color = Color.white;
    }
}
