using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaccoonCutScene : MonoBehaviour, IPointerClickHandler
{
    public int CutSceneCount = 10;
    public Sprite[] RCCutScenes = new Sprite[10];

    int curCutSceneCount;

    void Awake()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (CutSceneCount != 0)
        {
            curCutSceneCount = 0;
            GetComponent<Image>().color = new Color(1, 1, 1, 0);
            StartCoroutine(SceneChange(RCCutScenes[curCutSceneCount], GetComponent<RectTransform>().sizeDelta));
        }
        else
        {
            this.gameObject.SetActive(false);
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
            //StartCoroutine(SceneChange(RCCutScenes[curCutSceneCount], GetComponent<RectTransform>().sizeDelta));
            GetComponent<Image>().sprite = RCCutScenes[curCutSceneCount];
        else
        {
            this.gameObject.SetActive(false);
            GetComponentInParent<CutSceneControl>().CutSceneEnd();
        }
    }

    IEnumerator SceneChange(Sprite newSprite, Vector2 endSize)
    {
        GameObject newImageObj = new GameObject();
        Image newImage = newImageObj.AddComponent<Image>();
        newImage.sprite = newSprite;
        newImageObj.transform.SetParent(GameObject.Find("CutSceneCanvas").transform);
        newImageObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        Vector2 Size = endSize * 1.3f;
        float count = 0.0f;
        Color initColor = newImage.color;
        while (count <= 1.0f)
        {
            newImageObj.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(Size, endSize * 0.9f, count);
            //Debug.Log("Size = " + newImageObj.GetComponent<RectTransform>().sizeDelta);
            initColor.a = count;
            newImage.color = initColor;

            count += Time.deltaTime;

            yield return null;
        }

        GetComponent<Image>().sprite = newSprite;
        GetComponent<Image>().color = new Color(1, 1, 1, 1);
        DestroyImmediate(newImageObj);
    }


}
