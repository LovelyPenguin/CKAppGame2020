using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoubleClickDetect : MonoBehaviour
{
    public UnityEvent DoubleClickEvent;

    float clickedTime;

    private void Start()
    {
        clickedTime = 0;
    }


    private void OnMouseDown()
    {
        if ((Time.time - clickedTime) <= 0.3f)
        {
            DoubleClickEvent.Invoke();
            clickedTime = 0.0f;
        }
        else
        {
            clickedTime = Time.time;
        }    
    }
}
