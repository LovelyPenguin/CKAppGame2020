using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoubleClickDetect : MonoBehaviour
{
    public UnityEvent DoubleClickEvent;
    public UnityEvent NonDoubleClickEvent;

    bool clicked;
    float clickedTime;

    private void Start()
    {
        clicked = false;
        clickedTime = 0;
    }

    private void Update()
    {
        if(clicked)
            if(Time.time - clickedTime > 0.3f)
            {
                NonDoubleClickEvent.Invoke();
                clicked = false;
            }
    }

    private void OnMouseDown()
    {
        if (clicked)
        {
            if ((Time.time - clickedTime) <= 0.3f)
            {
                DoubleClickEvent.Invoke();
                clickedTime = 0.0f;
            }
            clicked = false;
        }
        else
        {
            clickedTime = Time.time;
            clicked = true;
        }
    }
}
