﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMng : MonoBehaviour
{
    public UnityEvent openEvent;
    public UnityEvent closeEvent;
    public bool isPopupMenuOpen = false;

    GameObject dustGenerator;
    GameObject RaccoonMng;

    public float openTime;
    [NonSerialized]
    public float setOpenTime;
    [SerializeField]
    private bool isOpen = false;

    public int money;
    [NonSerialized]
    public int customerCount;

    public bool getOpenData
    {
        get
        {
            return isOpen;
        }
    }

    private static GameMng instance;
    public static GameMng Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameMng>();
            }

            return instance;
        }
    }

    private void Awake()
    {
#if UNITY_STANDALONE_WIN
        Screen.SetResolution(384, 768, false);
#endif
        instance = this;
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dustGenerator = GameObject.Find("DustGenerator");
        RaccoonMng = GameObject.Find("RaccoonManager");
        closeEvent.AddListener(dustGenerator.GetComponent<DustGenerator>().Generate);
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen == true)
        {
            openTime -= 1 * Time.deltaTime;
            if (openTime <= 0 && isOpen == true)
            {
                CloseCafe();
            }
        }
        // 카메라 이동은 카메라 매니저에서 담당해 더 이상 필요 없는 부분입니다.
        //else
        //{
        //    if (RaccoonMng.GetComponent<RaccoonMng>().isRCOnDrag())
        //    {
        //        Debug.Log("1");
        //        //MoveScreenEdge();
        //    }
        //}
        ////MoveScreenEdge();
    }

    public void OpenCafe(float setOpenTime)
    {
        Debug.Log("Open");
        isOpen = true;
        openTime = setOpenTime;
        this.setOpenTime = setOpenTime;
        openEvent.Invoke();
    }

    public void CloseCafe()
    {
        Debug.Log("Close");
        isOpen = false;
        closeEvent.Invoke();
        customerCount = 0;
    }

    //public void MoveScreenEdge()
    //{
    //    float edge = 20.0f;
    //    UnityEngine.Vector3 mousePos = Input.mousePosition;
    //    UnityEngine.Vector3 Offset = new UnityEngine.Vector3(0, 0, 0);

    //    if (mousePos.x < edge && mousePos.x > 0)
    //    {
    //        Offset.x = -0.05f;
    //    }
    //    else if (Screen.width - mousePos.x < edge && mousePos.x < Screen.width)
    //    {
    //        Offset.x = 0.05f;
    //    }
    //    else
    //    {
    //        Offset.x = 0.0f;
    //    }
    //    if (mousePos.y < edge && mousePos.y > 0)
    //    {
    //        Offset.y = -0.05f;
    //    }
    //    else if (Screen.height - mousePos.y < edge && mousePos.y < Screen.height)
    //    {
    //        Offset.y = 0.05f;
    //    }
    //    else
    //    {
    //        Offset.y = 0.0f;
    //    }

    //    Camera.main.transform.Translate(Offset);

    //}
}
