using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableTimer : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private Vector2 previousMousePosition;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Timer Drag Begin");
        previousMousePosition = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (previousMousePosition.x < Input.mousePosition.x)
        {
            Debug.Log("Time Add");
            GameMng.Instance.openTime += 1;
        }
        else if (previousMousePosition.x > Input.mousePosition.x)
        {
            Debug.Log("Time Minus");
            if (GameMng.Instance.openTime > 0)
            {
                GameMng.Instance.openTime -= 1;
            }
        }
        previousMousePosition = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Timer Drag End");
    }
}
