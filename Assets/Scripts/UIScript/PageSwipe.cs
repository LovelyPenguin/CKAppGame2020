using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageSwipe : MonoBehaviour
{
    private RectTransform myRect;
    public int pages;
    public float[] pagesPosition;
    public float setCurrentPosX;

    private bool move;

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
            pagesPosition[i] = -count;
            count += 850;
        }
    }

    void Update()
    {
        if (move)
        {
            transform.localPosition = new Vector2(Mathf.Lerp(transform.localPosition.x, setCurrentPosX, Time.deltaTime * 10), 0);
        }
    }

    private void OnMouseUp()
    {
        Debug.Log("Penguin");
    }

    public void ValueChange()
    {
        if (gameObject.GetComponent<RectTransform>().localPosition.x <= 0 && gameObject.GetComponent<RectTransform>().localPosition.x > -425)
        {
            setCurrentPosX = 0;
        }

        if (gameObject.GetComponent<RectTransform>().localPosition.x <= -425 && gameObject.GetComponent<RectTransform>().localPosition.x > -1275)
        {
            setCurrentPosX = -850;
        }

        if (gameObject.GetComponent<RectTransform>().localPosition.x <= -1275 && gameObject.GetComponent<RectTransform>().localPosition.x > -1700)
        {
            setCurrentPosX = -1700;
        }
        move = true;
    }

    public void MovementDiable()
    {
        move = false;
    }
}
