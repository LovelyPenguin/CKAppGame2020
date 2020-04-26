using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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

        float edge = 10.0f;
        UnityEngine.Vector3 mousePos = Input.mousePosition;
        UnityEngine.Vector3 Offset = new UnityEngine.Vector3(0, 0, 0);

        if (mousePos.x < edge && mousePos.x > 0)
        {
            Offset.x = -0.1f;
        }
        else if (Screen.width - mousePos.x < edge && mousePos.x < Screen.width)
        {
            Offset.x = 0.1f;
        }
        else
        {
            Offset.x = 0.0f;
        }
        if (mousePos.y < edge && mousePos.y > 0)
        {
            Offset.y = -0.1f;
        }
        else if (Screen.height - mousePos.y < edge && mousePos.y < Screen.height)
        {
            Offset.y = 0.1f;
        }
        else
        {
            Offset.y = 0.0f;
        }

        Camera.main.transform.Translate(Offset);
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
