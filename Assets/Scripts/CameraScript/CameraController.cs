using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            //if (transform.position.y > SecondFloorViewHeight)
            //{
            //    GameMng.GetComponent<FloorStatMng>().SetFloor2();
            //}
            //else
            //    GameMng.GetComponent<FloorStatMng>().SetFloor1();
        //}
    }

    public void RememberPos()
    {
        initPos = transform.position;
    }

    public void RollbackPos()
    {
        transform.position = initPos;
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

    private void ZoomInOut()
    {
        float Delta = Input.mouseScrollDelta.y * 0.1f;

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
