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
    GameObject item;

    RaycastHit hit;

    void OnMouseDown()
    {
        originCoord = transform.position;
        transform.Translate(0, mDeltaY, 0);

        mZCoord = 1;
        item = Instantiate(Shadow) as GameObject;
        Physics.Raycast(transform.position - new UnityEngine.Vector3(0, mDeltaY, 0), transform.forward, out hit, Mathf.Infinity);
        item.transform.position = hit.point + new UnityEngine.Vector3(0, 0.1f, 0);
    }

    void OnMouseUp()
    {
        if (Physics.Raycast(transform.position - new UnityEngine.Vector3(0, mDeltaY, 0), transform.forward, out hit, Mathf.Infinity) && hit.transform.gameObject.tag == "Map")
        {
            transform.position = hit.point + new UnityEngine.Vector3(0,mDeltaY,0);
        }
        else
        {
            transform.position = originCoord;
        }
        Destroy(item);
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
        transform.position = GetMouseWorldPos();
        Physics.Raycast(transform.position - new UnityEngine.Vector3(0, mDeltaY, 0), transform.forward, out hit, Mathf.Infinity);
        item.transform.position = hit.point + new UnityEngine.Vector3(0, 0.1f, 0);
    }
}