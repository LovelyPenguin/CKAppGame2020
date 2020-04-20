using System;
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
public class DragObject : MonoBehaviour
{

    private UnityEngine.Vector3 mOffset;

    private float mZCoord;
    private float mDeltaY = 0.5f;
    private UnityEngine.Vector3 originCoord;

    RaycastHit hit;

    void OnMouseDown()
    {
        originCoord = transform.position;
        transform.Translate(0, mDeltaY, 0);
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    void OnMouseUp()
    {
        Debug.DrawRay(transform.position, transform.forward * 100, Color.blue, 0.3f);
        transform.Translate(0, -mDeltaY, 0);
        if (!Physics.Raycast(transform.position, transform.forward, out hit, 100))
        {
            transform.position = originCoord;
        }
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
        transform.position = GetMouseWorldPos() + mOffset;
    }
}