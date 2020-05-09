using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

// not completed
/*
    references:
    https://answers.unity.com/questions/45626/how-can-i-drag-an-object-along-uneven-terrain-on-i.html
    https://www.youtube.com/watch?v=0yHBDZHLRbQ  오브젝트 드래그
    https://www.youtube.com/watch?v=VZRkSUX6ubA
    https://chameleonstudio.tistory.com/63  레이캐스트 히트
*/
public class DragRaccoon : MonoBehaviour
{

    private UnityEngine.Vector3 mOffset;
    private UnityEngine.Vector3 originCoord;

    public float mZCoord;
    private float mDeltaY = 0.3f;
    public GameObject Shadow;
    GameObject Shadowinst;

    RaycastHit hit;

    void OnMouseDown()
    {
        Debug.Log("RCDrag_OnMouseDown");
        originCoord = transform.position;
        transform.Translate(0, mDeltaY, 0);

        mZCoord = 1;
        Shadowinst = Instantiate(Shadow) as GameObject;
        Physics.Raycast(transform.position - new UnityEngine.Vector3(0, mDeltaY, 0), transform.forward, out hit, Mathf.Infinity);
        Shadowinst.transform.position = transform.position + new UnityEngine.Vector3(0, -0.1f, 0);
    }

    void OnMouseUp()
    {
        Debug.Log("RCDrag_OnMouseUp");
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.tag == "Ground")
            {
                transform.position = hit.point + new UnityEngine.Vector3(0, mDeltaY, 0);
                Debug.Log("GroundHit");
            }
            else if (hit.transform.gameObject.tag == "WallL")
            {
                transform.position = hit.point + new UnityEngine.Vector3(0, 0, -1.5f);
                Debug.Log("WallLHit");
            }
            else if (hit.transform.gameObject.tag == "WallR")
            {
                transform.position = hit.point + new UnityEngine.Vector3(-1.5f, 0, 0);
                Debug.Log("WallRHit");
            }
            else
            {
                transform.position = originCoord;
                Debug.Log("ElseHit");
                Debug.Log(hit.collider.gameObject.name);
            }
        }
        else
        {
            transform.position = originCoord;
            Debug.Log("NothingHit");
        }
        Destroy(Shadowinst);
    }

    private UnityEngine.Vector3 GetMouseWorldPos()
    {
        UnityEngine.Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;
        mousePoint.y += mDeltaY;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        //Debug.Log("RCDrag_OnMouseDrag");
        transform.position = GetMouseWorldPos();
        Physics.Raycast(transform.position - new UnityEngine.Vector3(0, mDeltaY, 0), transform.forward, out hit, Mathf.Infinity);
        Shadowinst.transform.position = transform.position + new UnityEngine.Vector3(0, -0.1f, 0);
    }
}