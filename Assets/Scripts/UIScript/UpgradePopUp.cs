using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradePopUp : MonoBehaviour, IPointerClickHandler
{
    public GameObject[] RotateBack = new GameObject[2];
    public Sprite Star;

    public GameObject RC;

    GameObject[] Stars;
    int StarCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateBack[0].transform.Rotate(Vector3.forward, 1.0f);
        RotateBack[1].transform.Rotate(Vector3.forward, -1.0f);
    }

    public void RCUpgradePopUp(Sprite RCImage, int newRCRank)
    {
        RC.GetComponent<Image>().sprite = RCImage;

        switch (newRCRank)
        {
            case 2:
                Stars = new GameObject[3];
                StarCount = 3;

                for (int i = 0; i < StarCount; i++)
                {
                    Stars[i] = new GameObject();
                    Stars[i].transform.SetParent(transform);
                    Image img = Stars[i].AddComponent<Image>();
                    img.sprite = Star;
                }

                Stars[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -150);
                Stars[0].GetComponent<RectTransform>().sizeDelta = new Vector2(75, 75);

                Stars[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-70, -370);
                Stars[1].GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150);
                Stars[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(70, -370);
                Stars[2].GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150);

                break;

            case 3:
                Stars = new GameObject[5];
                StarCount = 5;

                for (int i = 0; i < StarCount; i++)
                {
                    Stars[i] = new GameObject();
                    Stars[i].transform.SetParent(transform);
                    Image img = Stars[i].AddComponent<Image>();
                    img.sprite = Star;
                }

                Stars[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-37, -150);
                Stars[0].GetComponent<RectTransform>().sizeDelta = new Vector2(75, 75);
                Stars[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(37, -150);
                Stars[1].GetComponent<RectTransform>().sizeDelta = new Vector2(75, 75);

                Stars[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, -370);
                Stars[2].GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150);
                Stars[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -370);
                Stars[3].GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150);
                Stars[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(100, -370);
                Stars[4].GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150);

                break;
        }
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        for(int i =0;i<StarCount;i++)
            DestroyImmediate(Stars[i]);

        gameObject.SetActive(false);
        GetComponentInParent<CutSceneControl>().CutSceneEnd();
    }
}
