using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetRCInfo : MonoBehaviour
{
    static int RaccoonCount = 7;
    static int RaccoonRankCount = 3;
    public GameObject RCImage;
    public Sprite[] Rc = new Sprite[RaccoonCount * RaccoonRankCount];
    public Sprite[] RcIcons = new Sprite[RaccoonCount];
    public GameObject RcIcon;
    RaccoonMng RCMng;
    public GameObject[] Stars = new GameObject[RaccoonRankCount];
    public Sprite[] StarSprite = new Sprite[2];

    private int CurrentRaccoon;

    public int GetCurRCChoice
    {
        get
        {
            return CurrentRaccoon;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentRaccoon = 1;
        RCMng = GameObject.Find("GameManager").GetComponent<RaccoonMng>();
        SetRaccoon(0);
    }

    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TailUpdate(InteractButton);
    }

    int Max(int a, int b)
    {
        return (a > b) ? a : b;
    }

    private void RaccoonImageUpdate()
    {
        int RCRank = RCMng.GetRCRank(CurrentRaccoon);
        RCImage.GetComponent<Image>().sprite = Rc[Max(CurrentRaccoon * RaccoonRankCount + RCRank - 1,0)];
        if (RCMng.GetComponent<RaccoonMng>().GetRCUnlockData(CurrentRaccoon))
        {
            if (RCRank < RaccoonRankCount)
            {
                RCImage.GetComponent<Image>().color = Color.white;
            }
            else
            {
                RCImage.GetComponent<Image>().color = Color.white;
            }
        }
        else
        {
            RCImage.GetComponent<Image>().sprite = Rc[CurrentRaccoon * RaccoonRankCount + RCRank];
            RCImage.GetComponent<Image>().color = Color.black;
        }
        SetStar(RCMng.GetComponent<RaccoonMng>().GetRCRank(CurrentRaccoon));
    }
    void SetStar(int num)
    {
        int i = 0;
        for (; i < num; i++)
        {
            Stars[i].GetComponent<Image>().sprite = StarSprite[0];
        }
        for(;i< RaccoonRankCount; i++)
        {
            Stars[i].GetComponent<Image>().sprite = StarSprite[1];
        }
    }

    private GameObject InteractButton;
    public GameObject ParentObj;
    public Font mfont;
    public Sprite Balloon;
    public Sprite BalloonTail;
    private GameObject Tail;

    GameObject CreateButton(float xPos, float yPos, float width, float height, string text, Font font, Transform parent)
    {
        Tail = new GameObject("Tail");
        Tail.transform.SetParent(parent);
        Tail.layer = 5;
        Image tailImg = Tail.AddComponent<Image>();
        tailImg.sprite = BalloonTail;

        GameObject newButton = new GameObject("UpgradeBtn");
        newButton.transform.SetParent(parent);
        newButton.layer = 5;
        newButton.transform.localScale = Vector3.one;

        RectTransform RectTnsf = newButton.AddComponent<RectTransform>();
        RectTnsf.anchoredPosition = new Vector2(xPos, yPos);
        RectTnsf.sizeDelta = new Vector2(width, height);

        Image Img = newButton.AddComponent<Image>();
        Img.sprite = Balloon;

        Button btn = newButton.AddComponent<Button>();
        btn.onClick.AddListener(RCInterAct);

        GameObject newTextObj = new GameObject("text");
        newTextObj.transform.SetParent(newButton.transform);
        newTextObj.layer = 5;
        newTextObj.AddComponent<RectTransform>();
        newTextObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        newTextObj.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        Text newText = newTextObj.AddComponent<Text>();
        newText.font = font;
        newText.fontSize = 40;
        newText.text = text;
        newText.color = Color.black;
        newText.alignment = TextAnchor.MiddleCenter;
        newText.transform.localScale = Vector3.one;

        TailUpdate(newButton);

        return newButton;
    }

    private void TailUpdate(GameObject Balloon)
    {
        if(Tail)
        {
            float Delta = GameObject.Find("RBtn_2").transform.localPosition.x - GameObject.Find("RBtn_1").transform.localPosition.x;

            Vector3 V1 = GameObject.Find("RCList").transform.localPosition;
            V1.x += (CurrentRaccoon - (int)RCMng.GetMaxRCcount() / 2) * Delta;

            GameObject RCMask = GameObject.Find("RCMask");
            Debug.Log("tail's end Point = " + V1);
            if (V1.x > RCMask.transform.localPosition.x + RCMask.GetComponent<RectTransform>().sizeDelta.x/2 || V1.x < RCMask.transform.localPosition.x - RCMask.GetComponent<RectTransform>().sizeDelta.x/2)
                DestroyUpgradeBtn();

            Vector3 V2 = Balloon.transform.localPosition;

            Debug.Log("tail's begin Point = " + V2);
            Vector3 V = V1 - V2;
            Debug.Log("tail's vector = " + V);

            float deg;
            Tail.transform.localPosition = Balloon.transform.localPosition + (V * 0.5f);
            Tail.GetComponent<RectTransform>().sizeDelta = new Vector2(10, V.magnitude/2);
            Tail.transform.eulerAngles = new Vector3(0,0, (deg = Mathf.Atan(V.y / V.x) * 180 / Mathf.PI + 90.0f) > 90 ? deg - 180.0f : deg);
        }
    }
    public GameObject RCInfoText;
    public void SetRaccoon(int index)
    {
        if (CurrentRaccoon == index)
        {
            if (!InteractButton && RCMng.GetRCRank(index) != RCMng.GetRCMaxRank())
            {
                string newTxt;
                int RCRank = RCMng.GetRCRank(CurrentRaccoon);
                if (RCMng.GetComponent<RaccoonMng>().GetRCUnlockData(CurrentRaccoon))
                {
                    if (RCRank < RaccoonRankCount)
                        newTxt = "업그레이드 " + RCMng.RetCost(CurrentRaccoon, RCRank).ToString() + "원";
                    else
                        newTxt = "업그레이드 완료";
                }
                else
                {
                    newTxt = "해금" + RCMng.RetCost(CurrentRaccoon, 0).ToString() + "원";
                }

                float XPos = (GameObject.Find("RCList").GetComponent<RectTransform>().localPosition.x + (index - (int)RCMng.GetMaxRCcount() / 2) * 220)/2;
                InteractButton = CreateButton(XPos, 300, 600, 200, newTxt, mfont, ParentObj.transform);
            }
            else if (InteractButton)
                DestroyUpgradeBtn();
        }
        else
        {
            DestroyUpgradeBtn();
            CurrentRaccoon = index;
            RcIcon.GetComponent<Image>().sprite = RcIcons[CurrentRaccoon];
        }
        RCInfoText.GetComponent<RCInfoText>().UpdateText(CurrentRaccoon);
        RaccoonImageUpdate();
    }

    public void RCInterAct()
    {
        DestroyUpgradeBtn();
        if (RCMng.GetComponent<RaccoonMng>().RaccoonUnlock[CurrentRaccoon])
        {
            RCUpgrade();
        }
        else
        {
            RCUnlock();
        }
    }

    public void DestroyUpgradeBtn()
    {
        if (InteractButton)
        {
            DestroyImmediate(InteractButton);
            DestroyImmediate(Tail);
        }
    }

    private void RCUpgrade()
    {
        RCMng.GetComponent<RaccoonMng>().UpgradeRC(CurrentRaccoon);
        RaccoonImageUpdate();
    }
    private void RCUnlock()
    {
        RCMng.GetComponent<RaccoonMng>().UnlockRC(CurrentRaccoon);
        RaccoonImageUpdate();
    }

    public void UpgradePopup(float YPos)
    {
        
    }
}
