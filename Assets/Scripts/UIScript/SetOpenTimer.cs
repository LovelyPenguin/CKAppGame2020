using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetOpenTimer : MonoBehaviour
{
    private Text myText;
    public float timer;
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
        myText.text = min.ToString() + ":" + sec.ToString();
        if (GameMng.Instance.getOpenData)
        {
            min = Mathf.FloorToInt(GameMng.Instance.openTime / 60);
            sec = Mathf.FloorToInt(GameMng.Instance.openTime % 60);
        }
    }

    public void PlusTime()
    {
        if (GameMng.Instance.getOpenData == false)
        {
            sec += 5;
            if (sec >= 60)
            {
                min++;
                sec = 0;
            }
        }
    }

    public void MinusTime()
    {
        if (GameMng.Instance.getOpenData == false && (min >= 0 && sec >= 0))
        {
            sec -= 5;
            if (sec < 0 && min - 1 > -1)
            {
                min--;
                sec = 55;
            }
        }
    }

    public void SendTimeData()
    {
        timer = (min * 60) + sec;
        GameMng.Instance.OpenCafe(timer);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
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
