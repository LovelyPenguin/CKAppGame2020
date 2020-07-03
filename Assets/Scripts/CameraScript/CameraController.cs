using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public GameObject GameMng;

    public GameObject Background;

    public bool isMoved;
    private Vector3 initPos;
    private float DefaultCameraSize;
    public float MaxCameraSize = 15;
    public float MinCameraSize = 5;

    public float SecondFloorViewHeight;

    public bool IsMoved()
    {
        bool retval = isMoved;
        isMoved = false;
        return retval;
    }

    // Start is called before the first frame update
    void Start()
    {
        isMoved = false;
        initPos = new Vector3(0, 0, 0);
        DefaultCameraSize = GetComponent<Camera>().orthographicSize;
        exist[0] = exist[1] = false;
    }

    // Update is called once per frame
    void Update()
    {
        ZoomInOut();
        //if (this.CompareTag("SubCamera"))
        //{
        //    transform.position = Camera.main.transform.position;
        //    GetComponent<Camera>().orthographicSize = Camera.main.GetComponent<Camera>().orthographicSize;
        //}
        //else
        //{
        if (transform.position.y > SecondFloorViewHeight)
        {
            //    GameMng.GetComponent<FloorStatMng>().SetFloor2();
        }
        else
        {
            //   GameMng.GetComponent<FloorStatMng>().SetFloor1();
        }
        //}
    }

    public void RememberPos()
    {
        initPos = transform.position;
    }

    public void RollbackPos()
    {
        //transform.position = initPos;
        StartCoroutine(ChangePos(initPos));
    }
    IEnumerator ChangePos(Vector3 Pos)
    {
        while (Vector3.Distance(transform.position, Pos) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, Pos, Time.deltaTime * 10);
            yield return null;
        }
        transform.position = Pos;
    }

    public void MoveScreenEdge()
    {
        float edge = 50.0f;
        UnityEngine.Vector3 mousePos = Input.mousePosition;
        UnityEngine.Vector3 Offset = new UnityEngine.Vector3(0, 0, 0);

        if (mousePos.x < edge && mousePos.x > 0)
        {
            Offset.x = -0.05f;
        }
        else if (Screen.width - mousePos.x < edge && mousePos.x < Screen.width)
        {
            Offset.x = 0.05f;
        }
        else
        {
            Offset.x = 0.0f;
        }
        if (mousePos.y < edge && mousePos.y > 0)
        {
            Offset.y = -0.05f;
        }
        else if (Screen.height - mousePos.y < edge && mousePos.y < Screen.height)
        {
            Offset.y = 0.05f;
        }
        else
        {
            Offset.y = 0.0f;
        }

        Camera.main.transform.Translate(Offset);
        isMoved = true;
    }


    private Vector2[] InitPos = new Vector2[2];
    private float InitDelta;
    private Vector2[] CurPos = new Vector2[2];
    private float CurDelta;
    private bool[] exist = new bool[2];

    private void ZoomInOut()
    {
        float Delta;
        // for mouse wheel input
        {
            Delta = Input.mouseScrollDelta.y * 0.1f;

        }
        // for touch input
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);
                if (t.phase == TouchPhase.Began)
                {
                    if (i < 2)
                    {
                        exist[i] = true;
                        InitPos[i] = t.position;
                    }
                }
                else if (t.phase == TouchPhase.Ended)
                {
                    if (i < 2)
                        CurPos[i] = t.position;
                }
                else if (t.phase == TouchPhase.Moved)
                {
                    if (i < 2)
                        exist[i] = false;
                }
            }

            if (exist[0] && exist[1])
            {
                InitDelta = (InitPos[1] - InitPos[0]).magnitude;
                CurDelta = (CurPos[1] - CurPos[0]).magnitude;

                Delta = Mathf.Abs(CurDelta - InitDelta);
            }
        }
        GetComponent<Camera>().orthographicSize += Delta;

        if (GetComponent<Camera>().orthographicSize > MaxCameraSize)
        {
            GetComponent<Camera>().orthographicSize = MaxCameraSize;
        }
        else if (GetComponent<Camera>().orthographicSize < MinCameraSize)
        {
            GetComponent<Camera>().orthographicSize = MinCameraSize;
        }
        Background.transform.localScale = new Vector3(GetComponent<Camera>().orthographicSize / 10, GetComponent<Camera>().orthographicSize / 10, 1);
    }
}
