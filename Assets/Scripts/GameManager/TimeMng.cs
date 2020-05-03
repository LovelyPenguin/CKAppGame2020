using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMng : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Application Start");
    }

    private void OnApplicationPause(bool pause)
    {
        Debug.Log("Application Pause");
    }

    private void OnDestroy()
    {
        Debug.Log("Application Close");
    }
}
