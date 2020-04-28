using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JuiceObject : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private GameObject mainSprite;
    [SerializeField]
    private GameObject decoSprite;
    private Vector2 initialValue;
    public bool isComplete = false;
    public float percentValue;

    // Start is called before the first frame update
    void Start()
    {
        initialValue = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetDecoSprite(bool isVisible)
    {
        decoSprite.SetActive(isVisible);
    }

    public void ResetAllValue()
    {
        isComplete = true;
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

    public void OnDrag(PointerEventData eventData)
    {
        if (isComplete)
        {
            Debug.Log(Input.mousePosition);
            gameObject.transform.position = Input.mousePosition;
            gameObject.transform.localScale = new Vector3(0.3f, 0.3f);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag End");
        gameObject.transform.position = initialValue;
        gameObject.transform.localScale = new Vector3(1f, 1f);
    }
}
