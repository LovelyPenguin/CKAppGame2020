using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RCListScroll : ScrollRect
{
    //private float minX = -220;
    //private float maxX = 220;

    //private float initMousePosX;
    //private float initXPos;
    //private float XPos;
    //private Vector2 newPos;

    // Start is called before the first frame update
    void Start()
    {
        base.vertical = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);

        GetComponent<AudioSource>().Play();
    }


    //public void OnDrag(PointerEventData eventData)
    //{
    //    float newXPos = initXPos + (eventData.position.x - initMousePosX) * 1530 / Screen.width;
    //    //Debug.Log("newXPos = " + newXPos);

    //    if (newXPos > maxX)
    //        newXPos = maxX;
    //    if (newXPos < minX)
    //        newXPos = minX;
    //    newPos.x = newXPos;
    //    GetComponent<RectTransform>().anchoredPosition = newPos;
    //    //Debug.Log("newPos = " + newPos);
    //}

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        GetComponent<AudioSource>().Stop();
    }

}
