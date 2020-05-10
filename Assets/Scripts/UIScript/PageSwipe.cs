using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageSwipe : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private RectTransform myRect;
    public int pages;
    public float[] pagesPosition;

    // Start is called before the first frame update
    void Start()
    {
        pages = transform.childCount;
        pagesPosition = new float[pages];
        myRect = GetComponent<RectTransform>();
        int count = 850;
        for (int i = 0; i < pages; i++)
        {
            if (i == 0)
            {
                pagesPosition[0] = 0;
                continue;
            }
            pagesPosition[i] = count;
            count += 850;
        }
    }

    void Update()
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Content Drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void ValueChange()
    {
        //Debug.Log("Change");
    }
}
