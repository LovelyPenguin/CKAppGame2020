// 버그 초, 분, 시로 잡지 않고 전체적인 타임으로 받고 주는게 좋을 듯!
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetOpenTimer : MonoBehaviour
{
    private Text myText;
    public float timer;

    [SerializeField]
    private GameObject[] buttons;
    private TimeSpan span;
    private bool secondLock = false;
    private int hour;

    // Start is called before the first frame update
    void Start()
    {
        myText = gameObject.GetComponent<Text>();
        span = new TimeSpan(0, 0, Mathf.FloorToInt(GameMng.Instance.openTime));
    }

    // Update is called once per frame
    void Update()
    {
        SetTimer();
    }

    void SetTimer()
    {
        if (true)
        {
            span = new TimeSpan(0, 0, Mathf.FloorToInt(GameMng.Instance.openTime));
            hour = Mathf.FloorToInt(GameMng.Instance.openTime / 3600);
        }

        string hourString;
        if (hour < 10)
        {
            hourString = '0' + hour.ToString();
        }
        else
        {
            hourString = hour.ToString();
        }

        string minString;
        if (span.Minutes < 10)
        {
            minString = '0' + span.Minutes.ToString();
        }
        else
        {
            minString = span.Minutes.ToString();
        }

        string secString;
        if (span.Seconds < 10 && !secondLock)
        {
            secString = '0' + span.Seconds.ToString();
        }
        else if (span.Seconds >= 10 && !secondLock)
        {
            secString = span.Seconds.ToString();
        }
        else
        {
            secString = "00";
        }

        myText.text = hourString + ":" + minString + ":" + secString;
    }

    public void PlusTime(int number)
    {
        if (GameMng.Instance.getOpenData == false)
        {
            GameMng.Instance.openTime += number;

            if (number >= 60)
            {
                secondLock = true;
            }
            else
            {
                secondLock = false;
            }
        }
    }

    public void MinusTime(int number)
    {
        if (GameMng.Instance.getOpenData == false)
        {
            if (GameMng.Instance.openTime - number >= 0)
            {
                GameMng.Instance.openTime -= number;
            }
            else
            {
                GameMng.Instance.openTime = 0;
            }

            if (number >= 60)
            {
                secondLock = true;
            }
            else
            {
                secondLock = false;
            }
        }
    }

    public void SendTimeData()
    {
        timer = GameMng.Instance.openTime;
        if (timer > 0)
        {
            GameMng.Instance.OpenCafe(timer);

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }
        }
    }

    public void ReturnButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(true);
        }
    }
}
