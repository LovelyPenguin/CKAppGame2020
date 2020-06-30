using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollViewSwap : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public PageSwipe child;

    public void OnPointerDown(PointerEventData eventData)
    {
        child.MovementDiable();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        child.ValueChange();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
