using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RCListScroll : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private float minX = -220;
    private float maxX = 220;

    private float initMousePosX;
    private float initXPos;
    private float XPos;
    private Vector2 newPos;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("RCList Clicked");
        initMousePosX = eventData.position.x;
        newPos = GetComponent<RectTransform>().anchoredPosition;
        initXPos = newPos.x;
        //Debug.Log("initXPos = " + initXPos);
    }


    public void OnDrag(PointerEventData eventData)
    {
        float newXPos = initXPos + (eventData.position.x - initMousePosX) * 1530 / Screen.width;
        //Debug.Log("newXPos = " + newXPos);

        if (newXPos > maxX)
            newXPos = maxX;
        if (newXPos < minX)
            newXPos = minX;
        newPos.x = newXPos;
        GetComponent<RectTransform>().anchoredPosition = newPos;
        //Debug.Log("newPos = " + newPos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

}
