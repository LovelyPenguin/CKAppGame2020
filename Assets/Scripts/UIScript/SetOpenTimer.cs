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
    public int hour;
    public int min;
    public int sec;

    [SerializeField]
    private GameObject[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        myText = gameObject.GetComponent<Text>();
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
            TimeSpan span;
            span = new TimeSpan(0, 0, Mathf.FloorToInt(GameMng.Instance.openTime));
            hour = span.Hours;
            min = span.Minutes;
            sec = span.Seconds;
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
        if (min < 10)
        {
            minString = '0' + min.ToString();
        }
        else
        {
            minString = min.ToString();
        }

        string secString;
        if (sec < 10)
        {
            secString = '0' + sec.ToString();
        }
        else
        {
            secString = sec.ToString();
        }

        myText.text = hourString + ":" + minString + ":" + secString;
    }

    public void PlusTime(int number)
    {
        if (GameMng.Instance.getOpenData == false)
        {
            //sec += number;
            //if (sec >= 60)
            //{
            //    min++;
            //    sec = 0;
            //}
            //if (min >= 60)
            //{
            //    hour++;
            //    min = 0;
            //}
            GameMng.Instance.openTime += number;
        }
    }

    public void MinusTime(int number)
    {
        if (GameMng.Instance.getOpenData == false && GameMng.Instance.openTime - number > 0)
        {
            GameMng.Instance.openTime -= number;
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
