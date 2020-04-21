using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SendDragData : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private RectTransform parentRect;
    private UIPopInOut menuMng;
    private float previusAnchorPosition;
    private float previousTouchPosY;

    public UnityEvent onDrag;

    // Start is called before the first frame update
    void Start()
    {
        parentRect = gameObject.transform.parent.GetComponent<RectTransform>();
        previusAnchorPosition = parentRect.anchoredPosition.y;
        menuMng = parentRect.GetComponent<UIPopInOut>();
    }

    void Update()
    {
        
    }

    public void OnDrag(PointerEventData data)
    {
        menuMng.isDrag = true;

        if (previousTouchPosY < data.position.y)
        {
            menuMng.isOpen = true;
            menuMng.ActivateCloseButton();
        }
        else if (previousTouchPosY > data.position.y)
        {
            menuMng.isOpen = false;
            menuMng.ActivateCloseButton();
        }

        previousTouchPosY = data.position.y;

        if (menuMng.isOpen)
        {
            onDrag.Invoke();
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        menuMng.isDrag = false;
    }
}
