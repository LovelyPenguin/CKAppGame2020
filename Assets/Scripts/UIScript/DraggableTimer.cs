using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableTimer : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private Vector2 previousMousePosition;
    public int value;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Timer Drag Begin");
        if (!GameMng.Instance.getOpenData)
        {
            previousMousePosition = Input.mousePosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!GameMng.Instance.getOpenData)
        {
            if (previousMousePosition.x < Input.mousePosition.x)
            {
                //Debug.Log("Time Add");
                GameMng.Instance.openTime += value;
            }
            else if (previousMousePosition.x > Input.mousePosition.x)
            {
                //Debug.Log("Time Minus");
                if (GameMng.Instance.openTime - value > 0)
                {
                    GameMng.Instance.openTime -= value;
                }
                else if (GameMng.Instance.openTime > 0)
                {
                    GameMng.Instance.openTime -= 1;
                }
            }
            previousMousePosition = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("Timer Drag End");
    }
}
