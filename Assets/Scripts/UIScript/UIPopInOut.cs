using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIPopInOut : MonoBehaviour
{
    private RectTransform myRect;
    private Vector2 defaultAnchorPosition;
    private Vector2 destinationAnchorPostion;
    private Camera mainCam;
    public bool isDrag;
    public bool isOpen;
    private float previusAnchorPosition;
    public GameObject cardExitPanel;
    [SerializeField]
    private float returnSpeed = 5f;
    [SerializeField]
    private int previousSiblingIndex;
    [SerializeField]
    private float openPos = 770;

    GameMng GMng;

    public UnityEvent PopIn;
    public UnityEvent PopOut;

    public UnityEvent PopOutEnd;

    public bool getIsOpen
    {
        get
        {
            return isOpen;
        }
    }

    void Start()
    {
        myRect = GetComponent<RectTransform>();
        defaultAnchorPosition = myRect.anchoredPosition;
        destinationAnchorPostion = new Vector2(myRect.anchoredPosition.x, openPos);
        previusAnchorPosition = myRect.anchoredPosition.y;
        mainCam = Camera.main;
        cardExitPanel.SetActive(false);

        GMng = GameObject.Find("GameManager").GetComponent<GameMng>();
    }

    void Update()
    {
        LimitPosition();
        UIPositionSetting();
        GMng.isPopupMenuOpen = isOpen;

        if (Vector2.Distance(myRect.anchoredPosition, defaultAnchorPosition) < 2f)
        {
            PopOutEnd.Invoke();
        }
    }

    //public void OnDrag(PointerEventData data)
    //{
    //    isDrag = true;
    //    DirectionYCheck();
    //    Vector2 dragPosition = data.position;
    //    dragPosition.x = myRect.position.x;
    //    myRect.position = dragPosition;
    //}

    //public void OnEndDrag(PointerEventData data)
    //{
    //    isDrag = false;
    //}

    private void DirectionYCheck()
    {
        if (previusAnchorPosition < myRect.anchoredPosition.y)
        {
            isOpen = true;
            GameMng.Instance.isPopupMenuOpen = isOpen;
            cardExitPanel.SetActive(true);
        }
        else if (previusAnchorPosition > myRect.anchoredPosition.y)
        {
            isOpen = false;
            GameMng.Instance.isPopupMenuOpen = isOpen;
        }
        previusAnchorPosition = myRect.anchoredPosition.y;
    }

    public void ActivateCloseButton()
    {
        cardExitPanel.SetActive(isOpen);
    }

    private void LimitPosition()
    {
        if (myRect.anchoredPosition.y > openPos)
        {
            myRect.anchoredPosition = destinationAnchorPostion;
        }
        else if (myRect.anchoredPosition.y < defaultAnchorPosition.y)
        {
            myRect.anchoredPosition = defaultAnchorPosition;
        }
    }

    private void UIPositionSetting()
    {
        if (isOpen && !isDrag)
        {
            myRect.anchoredPosition = Vector2.Lerp(myRect.anchoredPosition, destinationAnchorPostion, Time.deltaTime * returnSpeed);
            PopIn.Invoke();
        }

        if (!isOpen && !isDrag)
        {
            myRect.anchoredPosition = Vector2.Lerp(myRect.anchoredPosition, defaultAnchorPosition, Time.deltaTime * returnSpeed);
            PopOut.Invoke();
        }
    }

    public void OpenWindow()
    {
        isOpen = true;
        cardExitPanel.SetActive(true);
    }

    public void CloseWindow()
    {
        isOpen = false;
        cardExitPanel.SetActive(false);
        Debug.Log("Close Window");
    }

    public void OtherWindowClose()
    {
        isOpen = false;
    }

    public void ActiveCloseButton()
    {
        cardExitPanel.SetActive(true);
    }
}
