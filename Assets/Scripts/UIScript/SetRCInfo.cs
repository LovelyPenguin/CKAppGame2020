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
        RCMng = GameObject.Find("RaccoonManager").GetComponent<RaccoonMng>();
        CurrentRaccoon = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RaccoonImageUpdate()
    {
        int RCRank = RCMng.GetRCRank(CurrentRaccoon);
        RCImage.GetComponent<Image>().sprite = Rc[CurrentRaccoon * RaccoonRankCount + RCRank - 1];
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

    GameObject CreateButton(float xPos, float yPos, float width, float height, string text, Font font, Transform parent)
    {
        GameObject newButton = new GameObject("UpgradeBtn");
        newButton.transform.SetParent(parent);
        newButton.layer = 5;
        newButton.transform.localScale = Vector3.one;

        RectTransform RectTnsf = newButton.AddComponent<RectTransform>();
        RectTnsf.anchoredPosition = new Vector2(xPos, yPos);
        RectTnsf.sizeDelta = new Vector2(width, height);

        Image Img = newButton.AddComponent<Image>();

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

        return newButton;
    }
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

                InteractButton = CreateButton(0,-325, 600, 200, newTxt, mfont, ParentObj.transform);
            }
        }
        else
        {
            DestroyUpgradeBtn();
            CurrentRaccoon = index;
            RcIcon.GetComponent<Image>().sprite = RcIcons[CurrentRaccoon];
        }
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
            DestroyImmediate(InteractButton);
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
