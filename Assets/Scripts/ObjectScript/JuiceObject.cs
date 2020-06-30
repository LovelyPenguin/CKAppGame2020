using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class JuiceObject : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public UnityEvent startEvent;
    public UnityEvent endEvent;
    [SerializeField]
    private GameObject mainSprite;
    [SerializeField]
    private GameObject decoSprite;
    private Vector2 initialValue;
    [SerializeField]
    private GameObject icon;
    [SerializeField]
    private int juiceValue = 100;

    public bool isComplete = false;
    public string juiceName;
    public float percentValue;

    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        icon.SetActive(false);
        initialValue = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (icon.active)
        {
            icon.transform.position = Input.mousePosition;
        }
    }

    public void SetDecoSprite(bool isVisible)
    {
        if (decoSprite != null)
        {
            decoSprite.SetActive(isVisible);
        }
    }

    public void ResetAllValue()
    {
        isComplete = false;
        percentValue = 0f;
        mainSprite.SetActive(true);
        if (decoSprite != null)
        {
            decoSprite.SetActive(true);
        }
    }

    public void FillMainSprite()
    {
        if (mainSprite.GetComponent<Image>().fillAmount >= 1)
        {
            mainSprite.GetComponent<Image>().fillAmount = 0;
            return;
        }

        mainSprite.GetComponent<Image>().fillAmount += 0.35f;
    }

    public void FillMainSprite(int value)
    {
        mainSprite.GetComponent<Image>().fillAmount = value;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (isComplete)
        {
            startEvent.Invoke();
        }
    }

    void OnClickStay()
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isComplete)
        {
            icon.SetActive(true);
            icon.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag End");
        icon.SetActive(false);

        // 추후 라쿤 관련 클래스와 연계
        if (true)
        {
            Transform mouseVector = new GameObject().GetComponent<Transform>();
            mouseVector.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseVector.rotation = Camera.main.transform.rotation;

            Debug.DrawRay(mouseVector.position, mouseVector.forward * 30, Color.blue, 1f);

            if (Physics.Raycast(mouseVector.position, mouseVector.forward * 30, out hit) && hit.transform.GetComponent<DrinkTransfer>() != null)
            {
                if (hit.transform.GetComponent<DrinkTransfer>().selectJuice == juiceName)
                {
                    hit.transform.GetComponent<DrinkTransfer>().Detect(juiceValue * Mathf.RoundToInt(MoneyCalc(percentValue)));
                    if (percentValue >= 50)
                    {
                        hit.transform.GetComponent<Customer>().AddStamp();
                    }
                    ResetAllValue();
                    GameMng.Instance.GetComponent<DrinkMng>().UpdateContent();
                }
            }

            Destroy(mouseVector.gameObject);
        }
        endEvent.Invoke();
    }

    float MoneyCalc(float percent)
    {
        if (percent <= 100 && percent > 90)
        {
            return 1.5f;
        }
        else if (percent <= 90 && percent > 75)
        {
            return 1.25f;
        }
        else if (percent <= 75 && percent > 30)
        {
            return 1.0f;
        }
        else if (percent <= 30 && percent > 10)
        {
            return 0.75f;
        }
        else
        {
            return 0.5f;
        }
    }
}
