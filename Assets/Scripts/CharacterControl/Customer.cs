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
    void Start()
    {
        durationSecond = duration;
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
            
        }
        else
        {
            durationSecond -= GameMng.Instance.GetComponent<TimeMng>().flowTime;
            Debug.Log(GameMng.Instance.GetComponent<TimeMng>().flowTime);
        }
    }
}
