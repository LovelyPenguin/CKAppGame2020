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

    RaycastHit hit;

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

        // 추후 라쿤 관련 클래스와 연계
        if (true)
        {
            Transform mouseVector = new GameObject().GetComponent<Transform>();
            mouseVector.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseVector.rotation = Camera.main.transform.rotation;

            Debug.DrawRay(mouseVector.position, mouseVector.forward * 30, Color.blue, 1f);

            if (Physics.Raycast(mouseVector.position, mouseVector.forward * 30, out hit))
            {
                hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
                StartCoroutine(TestFunc(hit.transform.GetComponent<MeshRenderer>()));
                ResetAllValue();
                GameMng.Instance.GetComponent<DrinkMng>().UpdateContent();
            }

            Destroy(mouseVector.gameObject);
        }
    }

    IEnumerator TestFunc(MeshRenderer mesh)
    {
        yield return new WaitForSeconds(1f);
        mesh.material.color = Color.white;
    }
}
