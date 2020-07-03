using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoubleClickDetect : MonoBehaviour
{
    public UnityEvent DoubleClickEvent;
    public UnityEvent NonDoubleClickEvent;
    public UnityEvent LongClickEvent;

    bool clicked;
    float clickedTime;
    bool clicking;
    float clickingTime;

    private void Start()
    {
        clicked = false;
        clickedTime = 0;
        clicking = false;
        clickingTime = 0;
    }

    private void Update()
    {
        if(clicked)
            if(Time.time - clickedTime > 0.3f)
            {
                NonDoubleClickEvent.Invoke();
                clicked = false;
            }
        if(clicking)
        {
            clickingTime += Time.deltaTime;
            if(clickingTime > 0.3f)
            {
                LongClickEvent.Invoke();
                clicking = false;
            }
        }
    }

    private void OnMouseDown()
    {
        clicking = true;
        clickingTime = 0;
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

    private void OnMouseUp()
    {
        clicking = false;
    }
}
