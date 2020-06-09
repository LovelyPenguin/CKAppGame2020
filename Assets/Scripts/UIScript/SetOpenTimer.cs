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
        if (GameMng.Instance.getOpenData)
        {
            hour = Mathf.FloorToInt(GameMng.Instance.openTime / 3600);
            min = Mathf.FloorToInt(GameMng.Instance.openTime / 60);
            sec = Mathf.FloorToInt(GameMng.Instance.openTime % 60);
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
            sec += number;
            if (sec >= 60)
            {
                min++;
                sec = 0;
            }
            if (min >= 60)
            {
                hour++;
                min = 0;
            }
        }
    }

    public void MinusTime(int number)
    {
        if (GameMng.Instance.getOpenData == false && min >= 0)
        {
            if (sec != 0)
            {
                sec -= number;
            }
            if (sec < 0 && min - 1 > -1)
            {
                min--;
                sec = 55;
            }
            if (hour > 0 && min <= 0 && sec < 0)
            {
                hour--;
                min = 59;
                sec = 55;
            }
        }
    }

    public void SendTimeData()
    {
        timer = (hour * 3600) + (min * 60) + sec;
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
