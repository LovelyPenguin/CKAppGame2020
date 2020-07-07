using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
    public UnityEvent[] TutorialEvent;
    public int EventCount;
    private int curEventCount;
    public string[] Descriptions;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        curEventCount = 0;
        Debug.Log("Tutorial Started");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextEvent()
    {
        if (curEventCount < EventCount)
        {
            TutorialEvent[curEventCount++].Invoke();
            if (curEventCount == EventCount)
                gameObject.SetActive(false);
        }
    }
}
