using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool isMoved;
    private Vector3 initPos;

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
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
