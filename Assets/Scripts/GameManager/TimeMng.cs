using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeMng : MonoBehaviour
{
    public Text debugText;
    public float flowTime;

    private void Awake()
    {
        Debug.Log(PlayerPrefs.GetString("SaveLastTime"));
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            PlayerPrefs.SetString("SaveLastTime", System.DateTime.Now.ToString());
            Debug.Log("Save Time Data");
        }
        else
        {
            string lastTime = PlayerPrefs.GetString("SaveLastTime");
            System.DateTime lastDataTime = System.DateTime.Parse(lastTime);
            System.TimeSpan compareTime = System.DateTime.Now - lastDataTime;

            float day = compareTime.Days * 86400;
            float hour = compareTime.Hours * 3600;
            float min = compareTime.Minutes * 60;
            float sec = compareTime.Seconds;
            Debug.Log("Connect Time : " + compareTime.Seconds);

            if (debugText != null)
            {
                float time = hour + min + sec;
                debugText.text = "마지막 접속으로 부터 : " + time + " sec";
                flowTime = time;
            }
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetString("SaveLastTime", System.DateTime.Now.ToString());
        Debug.Log(PlayerPrefs.GetString("SaveLastTime"));
    }

    public void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
    }
    
    public void ResetData(string keyValue)
    {
        PlayerPrefs.DeleteKey(keyValue);
    }
}
