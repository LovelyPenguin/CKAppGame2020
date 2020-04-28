using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetRCInfo : MonoBehaviour
{
    static int RaccoonCount = 5;
    public Sprite[] Rc = new Sprite[RaccoonCount];
    GameObject RCMng;
    public GameObject[] Stars = new GameObject[5];
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
        RCMng = GameObject.Find("RaccoonManager");
        CurrentRaccoon = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RaccoonImageUpdate()
    {
        GetComponent<Image>().sprite = Rc[CurrentRaccoon];
        if (RCMng.GetComponent<RaccoonMng>().GetRCUnlockData(CurrentRaccoon))
            GetComponent<Image>().color = Color.white;
        else
            GetComponent<Image>().color = Color.black;
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

    public void SetRaccoon1()
    {
        CurrentRaccoon = 0;
        RaccoonImageUpdate();
    }
    public void SetRaccoon2()
    {
        CurrentRaccoon = 1;
        RaccoonImageUpdate();
    }
    public void SetRaccoon3()
    {
        CurrentRaccoon = 2;
        RaccoonImageUpdate();
    }
    public void SetRaccoon4()
    {
        CurrentRaccoon = 3;
        RaccoonImageUpdate();
    }
    public void SetRaccoon5()
    {
        CurrentRaccoon = 4;
        RaccoonImageUpdate();
    }

    public void RCUpgrade()
    {
        RCMng.GetComponent<RaccoonMng>().UpgradeRC(CurrentRaccoon);
        RaccoonImageUpdate();
    }

}
