using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetRCInfo : MonoBehaviour
{
    static int RaccoonCount;
    public GameObject RCImage;
    public Sprite[] Rc = new Sprite[RaccoonCount];
    RaccoonMng RCMng;
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
        RCMng = GameObject.Find("RaccoonManager").GetComponent<RaccoonMng>();
        CurrentRaccoon = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RaccoonImageUpdate()
    {
        RCImage.GetComponent<Image>().sprite = Rc[CurrentRaccoon];
        if (RCMng.GetComponent<RaccoonMng>().GetRCUnlockData(CurrentRaccoon))
        {
            int RCRank = RCMng.GetRCRank(CurrentRaccoon);
            if (RCRank < 5)
            {
                RCImage.GetComponent<Image>().color = Color.white;
                txt.text = "업그레이드 " + RCMng.RetCost(CurrentRaccoon, RCRank).ToString() + "원";
            }
            else
            {
                RCImage.GetComponent<Image>().color = Color.white;
                txt.text = "업그레이드 완료";
            }
        }
        else
        {
            RCImage.GetComponent<Image>().color = Color.black;
            txt.text = "해금" + RCMng.RetCost(CurrentRaccoon, 0).ToString() + "원";
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
        for(;i<5;i++)
        {
            Stars[i].GetComponent<Image>().sprite = StarSprite[1];
        }
    }

    public void SetRaccoon(int index)
    {
        CurrentRaccoon = index;
        RaccoonImageUpdate();
    }

    public void RCInterAct()
    {
        if(RCMng.GetComponent<RaccoonMng>().RaccoonUnlock[CurrentRaccoon])
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
        RCMng.GetComponent<RaccoonMng>().UpgradeRC(CurrentRaccoon);
        RaccoonImageUpdate();
    }
    private void RCUnlock()
    {
        RCMng.GetComponent<RaccoonMng>().UnlockRC(CurrentRaccoon);
        RaccoonImageUpdate();
    }
}
