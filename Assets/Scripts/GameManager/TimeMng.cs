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

            Debug.Log("GameQuit! " + GameMng.Instance.openTime);
            PlayerPrefs.SetFloat("GAMETIME", GameMng.Instance.openTime);
        }
        else
        {
            string lastTime = PlayerPrefs.GetString("SaveLastTime");
            System.DateTime lastDataTime = System.DateTime.Parse(lastTime);
            System.TimeSpan compareTime = System.DateTime.Now - lastDataTime;

            Debug.Log("Connect Time : " + compareTime.Seconds);

            // 쓸땐 주석 지워줄 것
            //GameMng.Instance.openTime = PlayerPrefs.GetFloat("GAMETIME") - compareTime.Seconds;
            //GameMng.Instance.setOpenTime = PlayerPrefs.GetFloat("FIRSTOPENTIME");

            if (debugText != null)
            {
                float time = compareTime.Seconds;
                debugText.text = "마지막 접속으로 부터 : " + time + " sec";
                flowTime = time;
            }
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("SaveLastTime", System.DateTime.Now.ToString());
        Debug.Log("Save Time Data");

        Debug.Log("GameQuit! " + GameMng.Instance.openTime);
        PlayerPrefs.SetFloat("GAMETIME", GameMng.Instance.openTime);
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
