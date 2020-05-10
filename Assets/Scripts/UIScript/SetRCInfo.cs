using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetRCInfo : MonoBehaviour
{
    static int RaccoonCount = 10;
    RaccoonMng RCMng;
    Image RCImage;

    public GameObject RCPreview;
    public Sprite[] Rc = new Sprite[RaccoonCount];
    public GameObject[] Stars = new GameObject[5];
    public Sprite[] StarSprite = new Sprite[2];
    public Text txt;

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
        RCImage = RCPreview.GetComponent<Image>();
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

        RCImage.sprite = Rc[CurrentRaccoon];
        if (RCMng.GetRCUnlockData(CurrentRaccoon))
        {
            RCImage.color = Color.white;
            if (RCRank == 5)
                txt.text = "업그레이드 완료";
            else
                txt.text = "업그레이드 " + RCMng.GetRCCost(CurrentRaccoon, RCRank).ToString() + "원";

        }
        else
        {
            RCImage.color = Color.black;
            txt.text = "해금 " + RCMng.GetRCCost(CurrentRaccoon, RCRank).ToString() + "원";
        }
        SetStar(RCRank);
    }
    void SetStar(int num)
    {
        int i = 0;
        for (; i < num; i++)
        {
            Stars[i].GetComponent<Image>().sprite = StarSprite[0];
        }
        for(;i<5;i++)
        {
            Stars[i].GetComponent<Image>().sprite = StarSprite[1];
        }
    }

    public void SetRaccoon(int RCNum)
    {
        CurrentRaccoon = RCNum;
        RaccoonImageUpdate();
    }

    public void RCInterAct()
    {
        if(RCMng.RaccoonUnlock[CurrentRaccoon])
        {
            RCUpgrade();
        }
        else
        {
            RCUnlock();
        }
    }
    private void RCUpgrade()
    {
        RCMng.UpgradeRC(CurrentRaccoon);
        RaccoonImageUpdate();
    }
    private void RCUnlock()
    {
        RCMng.UnlockRC(CurrentRaccoon);
        RaccoonImageUpdate();
    }
}
