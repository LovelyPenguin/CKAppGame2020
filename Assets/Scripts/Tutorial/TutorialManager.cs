using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public UnityEvent[] TutorialEvent;
    public int EventCount;
    private int curEvent;
    public string[] Descriptions;
    public RCSpriteState[] RCImageState;
    public int[] DecriptionCount;
    private int curDesciption;
    private int curSubDecription;

    public GameObject TextObj;
    public GameObject TextBox;
    public GameObject RaccoonImg;

    [SerializeField]
    public enum RCSpriteState
    { Normal = 0, bolRed, sad, bbanzzac };

    public Sprite[] RCImages;


    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        curEvent = 0;
        curDesciption = 0; 
        curSubDecription = 0;
        Debug.Log("Tutorial Started");
        NextEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextEvent()
    {
        if (curSubDecription < DecriptionCount[curEvent])
        {
            TextBox.SetActive(true);
            RaccoonImg.SetActive(true);
            RaccoonImg.GetComponent<Image>().sprite = RCImages[(int)RCImageState[curDesciption]];
            TextObj.GetComponent<Text>().text = Descriptions[curDesciption++];
            curSubDecription++;
            Debug.Log("tutorial Text Out");
        }
        else if (curEvent == EventCount)
            gameObject.SetActive(false);
        else if (curEvent < EventCount)
        {
            //TextBox.SetActive(false);
            //RaccoonImg.SetActive(false);
            TutorialEvent[curEvent++].Invoke();
            curSubDecription = 0;

            Debug.Log("tutorial Event Call");
        }
    }
}
