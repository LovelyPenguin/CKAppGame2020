using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMng : MonoBehaviour
{
    public UnityEvent openEvent;
    public UnityEvent closeEvent;

    GameObject dustGenerator;

    private float openTime;
    private bool isOpen = false;

    public int money;
    public bool getOpenData
    {
        get
        {
            return isOpen;
        }
    }
    public float getOpenTime
    {
        get
        {
            return openTime;
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
                dustGenerator.GetComponent<DustGenerator>().Generate();
            }
        }
    }

    public void OpenCafe(float setOpenTime)
    {
        Debug.Log("Open");
        isOpen = true;
        openTime = setOpenTime;
        openEvent.Invoke();
    }

    public void CloseCafe()
    {
        Debug.Log("Close");
        isOpen = false;
        closeEvent.Invoke();
    }
}
