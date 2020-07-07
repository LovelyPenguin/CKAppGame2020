using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
class GSaveData
{
    public int MONEY;
    public int CUSTOMERCOUNT;
}

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

    public AudioClip[] BGMs;
    public AudioClip[] SEs;

    public GameObject Tutorial;

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
        switch (PlayerPrefs.GetInt("OPENSTATUS"))
        {
            case 0:
                isOpen = false;
                GetComponents<AudioSource>()[0].clip = BGMs[0];
                break;
            case 1:
                isOpen = true;
                GetComponents<AudioSource>()[0].clip = BGMs[1];
                break;
        }
        dustGenerator = GameObject.Find("DustGenerator");
        RaccoonMng = GameObject.Find("GameManager"); 
        closeEvent.AddListener(dustGenerator.GetComponent<DustGenerator>().Generate);
        GetComponents<AudioSource>()[0].Play();

        LoadGame();
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
        PlayerPrefs.SetInt("OPENSTATUS", 1);
        openTime = setOpenTime;
        this.setOpenTime = setOpenTime;
        PlayerPrefs.SetFloat("FIRSTOPENTIME", this.setOpenTime);
        GetComponents<AudioSource>()[0].clip = BGMs[1];
        GetComponents<AudioSource>()[0].time = 0;
        GetComponents<AudioSource>()[0].Play();

        GetComponents<AudioSource>()[1].clip = SEs[1];
        GetComponents<AudioSource>()[1].Play();

        openEvent.Invoke();
    }

    public void CloseCafe()
    {
        Debug.Log("Close");
        isOpen = false;
        PlayerPrefs.SetInt("OPENSTATUS", 0);
        GetComponents<AudioSource>()[0].clip = BGMs[0];
        GetComponents<AudioSource>()[0].time = 0;
        GetComponents<AudioSource>()[0].Play();

        GetComponents<AudioSource>()[1].clip = SEs[0];
        GetComponents<AudioSource>()[1].Play();

        closeEvent.Invoke();
        customerCount = 0;
        //openTime = 600;
        openTime = 0;
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

    public void LoadGame()
    {
        GSaveData save = new GSaveData();
        if (gameObject.GetComponent<SaveLoader>().LoadData<GSaveData>(ref save, "GMNG"))
        {
            money = save.MONEY;
            customerCount = save.CUSTOMERCOUNT;
        }
        else
        {
            GameObject.Find("CutSceneCanvas").GetComponent<CutSceneControl>().CutSceneStart(8);
        }
    }

    public void SaveGame()
    {
        GSaveData save = new GSaveData();
        save.MONEY = money;
        save.CUSTOMERCOUNT = customerCount;
        gameObject.GetComponent<SaveLoader>().SaveData<GSaveData>(ref save, "GMNG");
    }

    public void TutorialStart()
    {
        Tutorial.SetActive(true);
    }
}
