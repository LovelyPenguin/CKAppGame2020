using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaccoonMng : MonoBehaviour
{
    private int selectedRC;
    private static int RaccoonCount = 5;
    public GameObject[] RC = new GameObject[RaccoonCount];
    GameObject[] Raccoon = new GameObject[RaccoonCount];
    bool[] RaccoonExist = new bool[RaccoonCount];
    bool[] RaccoonUnlock = new bool[RaccoonCount];
    int[] RaccoonRank = new int[RaccoonCount];

    // Start is called before the first frame update
    void Start()
    {
        UnlockRC(0);
        UnlockRC(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateRaccoon()
    {
        if (!GameObject.Find("GameManager").GetComponent<GameMng>().getOpenData)
        {
            selectedRC = GameObject.Find("RaccoonPreview").GetComponent<SetRCInfo>().GetCurRCChoice;

            Debug.Log(selectedRC); 
            
            if (!RaccoonExist[selectedRC])
            {
                Raccoon[selectedRC] = Instantiate(RC[selectedRC]) as GameObject;
                RaccoonExist[selectedRC] = true;
            }
        }
    }

    public bool isRCOnDrag()
    {
        for (int i = 0; i < RaccoonCount; i++)
        {
            if (RaccoonExist[i] && RC[i].GetComponent<RaccoonController>().GetIsDrag())
            {
                Debug.Log("Raccoon is moving");
                return true;
            }
        }
        return false;
    }

    public bool GetRCUnlockData(int index)
    {
        return RaccoonUnlock[index];
    }

    public int GetRCRank(int index)
    {
        return RaccoonRank[index];
    }

    public void UpgradeRC(int index)
    {
        if (RaccoonRank[index] < 5 && RaccoonUnlock[index])
            RaccoonRank[index]++;
    }

    public void UnlockRC(int index)
    {
        RaccoonUnlock[index] = true;
        RaccoonRank[index] = 1;
    }
}
