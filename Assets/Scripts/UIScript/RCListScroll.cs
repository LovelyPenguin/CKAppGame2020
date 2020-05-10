using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RCListScroll : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float minX;
    public float maxX;

    private float initMousePosX;
    private float initXPos;
    private float XPos;
    //private RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        XPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("RCList Clicked");
        initMousePosX = eventData.position.x;
        initXPos = transform.position.x;
        Debug.Log(initXPos);
    }


    public void OnDrag(PointerEventData eventData)
    {
        float newXPos = initXPos + eventData.position.x - initMousePosX;
        Debug.Log(newXPos);

        if ((newXPos < (XPos + maxX)) && (newXPos > (XPos - minX)))
            transform.position = new Vector3(initXPos + eventData.position.x - initMousePosX, transform.position.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(GetComponent<RectTransform>().anchoredPosition.x);
        Debug.Log("RCList Drag Ended");
    }

}
