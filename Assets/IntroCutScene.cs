using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IntroCutScene : MonoBehaviour, IPointerClickHandler
{
    public int CutSceneCount = 10;
    public Sprite[] CutScenes = new Sprite[10];

    public UnityEvent endCalls;

    int curCutSceneCount;

    void Awake()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(640, 1138);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (CutSceneCount != 0)
        {
            curCutSceneCount = 0;
            GetComponent<Image>().color = new Color(1, 1, 1, 1);
            GetComponent<Image>().sprite = CutScenes[curCutSceneCount];
        }
        else
        {
            this.gameObject.SetActive(false);
            endCalls.Invoke();
            GetComponentInParent<CutSceneControl>().CutSceneEnd();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (++curCutSceneCount < CutSceneCount)
            StartCoroutine(CSceneChange(CutScenes[curCutSceneCount]));
        else
        {
            endCalls.Invoke();
            this.gameObject.SetActive(false);
            GetComponentInParent<CutSceneControl>().CutSceneEnd();
        }
    }

    IEnumerator CSceneChange(Sprite newSprite)
    {
        GameObject newImageObj = new GameObject();
        Image newImage = newImageObj.AddComponent<Image>();
        newImage.sprite = newSprite;
        newImageObj.transform.SetParent(GameObject.Find("CutSceneCanvas").transform);
        newImageObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        newImageObj.GetComponent<RectTransform>().sizeDelta = new Vector2(640, 1138);
        newImageObj.transform.localScale = Vector3.one;

        float count = 0.0f;
        Color initColor = newImage.color;
        while (count <= 1.0f)
        {
            Debug.Log("Size = " + newImageObj.GetComponent<RectTransform>().sizeDelta);
            initColor.a = count;
            newImage.color = initColor;

            count += Time.deltaTime * 2.0f;

            yield return null;
        }

        GetComponent<Image>().sprite = newSprite;
        GetComponent<Image>().color = new Color(1, 1, 1, 1);
        DestroyImmediate(newImageObj);
    }
}
