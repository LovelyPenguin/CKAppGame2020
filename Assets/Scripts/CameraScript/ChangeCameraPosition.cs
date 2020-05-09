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

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        cameraInitialValueY = mainCam.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(mainCam.GetComponent<CameraController>().IsMoved())
        {
            cameraInitialValueY = mainCam.transform.position.y;
        }
    }

    public void PopInCamera()
    {
        if (mainCam.transform.position.y != cameraMovePositionY)
            mainCam.transform.position = new Vector3(
                mainCam.transform.position.x,
                Mathf.Lerp(mainCam.transform.position.y, cameraMovePositionY, Time.deltaTime * returnSpeed),
                mainCam.transform.position.z);
    }

    public void PopOutCamera()
    {
        if (mainCam.transform.position.y != cameraInitialValueY)
            mainCam.transform.position = new Vector3(
                mainCam.transform.position.x,
                Mathf.Lerp(mainCam.transform.position.y, cameraInitialValueY, Time.deltaTime * returnSpeed),
                mainCam.transform.position.z);
    }

   
}
