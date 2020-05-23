﻿using System.Collections;
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
        decoSprite.SetActive(isVisible);
    }

    public void ResetAllValue()
    {
        isComplete = false;
        percentValue = 0f;
        mainSprite.SetActive(true);
        decoSprite.SetActive(true);
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
                hit.transform.GetComponent<DrinkTransfer>().Detect(juiceValue);
                ResetAllValue();
                GameMng.Instance.GetComponent<DrinkMng>().UpdateContent();
            }

            Destroy(mouseVector.gameObject);
        }
        endEvent.Invoke();
    }

    IEnumerator TestFunc(MeshRenderer mesh)
    {
        yield return new WaitForSeconds(1f);
        mesh.material.color = Color.white;
    }
}
