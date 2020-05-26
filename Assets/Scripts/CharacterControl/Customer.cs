using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public bool isOpen;
    public float currentCafeTime;
    public int duration;

    private float durationSecond;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("START");
        durationSecond = duration * 60;
    }

    // Update is called once per frame
    void Update()
    {
        OpenCheck();
        DurationCheck();
    }

    void OpenCheck()
    {
        isOpen = GameMng.Instance.getOpenData;
        currentCafeTime = GameMng.Instance.openTime;

        if (isOpen == false)
        {

        }
        else
        {

        }
    }

    void DurationCheck()
    {
        if (isOpen)
        {
            durationSecond -= Time.deltaTime;
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("Game Stop");
        }
        else
        {
            Debug.Log("Game Restart");
            durationSecond -= GameMng.Instance.GetComponent<TimeMng>().flowTime;
            Debug.Log(GameMng.Instance.GetComponent<TimeMng>().flowTime);
        }
    }
}
