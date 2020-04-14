using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPopInOut : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public RectTransform myRect;

    private Vector2 defaultAnchorPosition;
    private Vector2 destinationAnchorPostion;
    private Camera mainCam;
    private bool isDrag = false;
    private bool isOpen = false;
    private float previusAnchorPosition;

    void Start()
    {
        myRect = GetComponent<RectTransform>();
        defaultAnchorPosition = myRect.anchoredPosition;
        destinationAnchorPostion = new Vector2(myRect.anchoredPosition.x, 795);
        previusAnchorPosition = myRect.anchoredPosition.y;
        mainCam = Camera.main;
    }

    void Update()
    {
        //if (!isDrag)
        //{
        //    myRect.anchoredPosition = Vector2.Lerp(myRect.anchoredPosition, defaultAnchorPosition, Time.deltaTime * 5f);
        //}

        //if (isOpen)
        //{
        //    myRect.anchoredPosition = Vector2.Lerp(myRect.anchoredPosition, destinationAnchorPostion, Time.deltaTime * 5f);
        //}

        DirectionYCheck();
        LimitPosition();
        UIPositionSetting();
    }

    public void OnDrag(PointerEventData data)
    {
        //isDrag = true;
        //Debug.Log("Drag Value: " + (data.pressPosition - data.position));
        //Debug.Log("Drag Position: " + data.position);

        //if (myRect.anchoredPosition.y > (destinationAnchorPostion.y - defaultAnchorPosition.y) / 2)
        //{
        //    Debug.Log("Open");
        //    isOpen = true;
        //}

        Vector2 dragPosition = data.position;
        dragPosition.x = myRect.position.x;
        myRect.position = dragPosition;
    }

    public void OnEndDrag(PointerEventData data)
    {
        isDrag = false;
    }

    private void DirectionYCheck()
    {
        if (previusAnchorPosition < myRect.anchoredPosition.y)
        {
            Debug.Log("Up!");
            isOpen = true;
        }
        else if (previusAnchorPosition > myRect.anchoredPosition.y)
        {
            Debug.Log("Down!");
            isOpen = false;
        }
        previusAnchorPosition = myRect.anchoredPosition.y;
    }

    private void LimitPosition()
    {
        if (myRect.anchoredPosition.y > 795)
        {
            myRect.anchoredPosition = destinationAnchorPostion;
        }
    }

    private void UIPositionSetting()
    {
        if (/*myRect.anchoredPosition.y > (destinationAnchorPostion.y - defaultAnchorPosition.y) / 2 ||*/ isOpen)
        {
            //Debug.Log("Open");
            myRect.anchoredPosition = Vector2.Lerp(myRect.anchoredPosition, destinationAnchorPostion, Time.deltaTime * 5f);
        }

        else if (/*myRect.anchoredPosition.y <= (destinationAnchorPostion.y - defaultAnchorPosition.y) / 2 ||*/ !isOpen)
        {
            //Debug.Log("Close");
            myRect.anchoredPosition = Vector2.Lerp(myRect.anchoredPosition, defaultAnchorPosition, Time.deltaTime * 5f);
        }
    }
}
