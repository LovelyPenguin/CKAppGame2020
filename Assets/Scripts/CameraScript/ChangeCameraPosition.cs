using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraPosition : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField]
    public float returnSpeed;
    [SerializeField]
    public float cameraMovePositionY;
    private float cameraInitialValueY;
    CameraController CmrCtrlr;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        cameraInitialValueY = mainCam.transform.position.y;
        CmrCtrlr = mainCam.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        // 임시조치
        //if(mainCam.GetComponent<CameraController>().IsMoved())
        //{
        //    cameraInitialValueY = mainCam.transform.position.y;
        //}
    }

    public void PopInCamera()
    {
        if (!CmrCtrlr.IsMoved())
        {
            if (mainCam.transform.position.y != cameraMovePositionY)
                mainCam.transform.position = new Vector3(
                    mainCam.transform.position.x,
                    Mathf.Lerp(mainCam.transform.position.y, cameraMovePositionY, Time.deltaTime * returnSpeed),
                    mainCam.transform.position.z);
        }
        else
        {
            cameraInitialValueY = mainCam.transform.position.y;
        }
    }

    public void PopOutCamera()
    {
        if (!CmrCtrlr.IsMoved())
        {
            if (mainCam.transform.position.y != cameraInitialValueY)
                mainCam.transform.position = new Vector3(
                    mainCam.transform.position.x,
                    Mathf.Lerp(mainCam.transform.position.y, cameraInitialValueY, Time.deltaTime * returnSpeed),
                    mainCam.transform.position.z);
        }
        else
        {
            cameraInitialValueY = mainCam.transform.position.y;
        }
    }

   
}
