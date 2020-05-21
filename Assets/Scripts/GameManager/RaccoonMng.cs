using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaccoonMng : MonoBehaviour
{
    GameMng GMng;
    private int selectedRC;
    private static int RaccoonCount = 10;
    public GameObject[] RC = new GameObject[RaccoonCount];
    bool[] RaccoonExist = new bool[RaccoonCount];
    public bool[] RaccoonUnlock = new bool[RaccoonCount];
    int[] RaccoonRank = new int[RaccoonCount];

    // Start is called before the first frame update
    void Start()
    {
        GMng = GameObject.Find("GameManager").GetComponent<GameMng>();
        UnlockRC(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(isRCOnDrag())
        {
            Camera.main.GetComponent<CameraController>().MoveScreenEdge();
        }
    }

    public void GenerateRaccoon(int RCindex)
    {
        if (!GameObject.Find("GameManager").GetComponent<GameMng>().getOpenData)
        {
            Debug.Log(RCindex); 
            
            if (!RaccoonExist[RCindex])
            {
                RC[RCindex].transform.position = new Vector3(5, 0, 5);
                RC[RCindex].GetComponent<RaccoonController>().SetRCActive(true);
                RaccoonExist[RCindex] = true;
                Debug.Log("Raccoon Created!");
            }
        }
    }

    public bool isRCOnDrag()
    {
        for (int i = 0; i < RaccoonCount; i++)
        {
            if (RC[i].GetComponent<RaccoonController>().GetIsDrag())
            {
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
        int cost = RetCost(index, RaccoonRank[index]);
        if (RaccoonRank[index] < 5 && RaccoonUnlock[index] && GMng.money >= cost)
        {
            RaccoonRank[index]++;
            GMng.money -= cost;
        }
    }

    public void UnlockRC(int index)
    {
        int cost = RetCost(index, 0);
        if(!RaccoonUnlock[index] && GMng.money >= cost)
        {
            RaccoonUnlock[index] = true;
            RaccoonRank[index] = 1;
            GMng.money -= cost;
            GenerateRaccoon(index);
        }
    }

    public void StartRCWork()
    {
        for(int i =0;i<RaccoonCount;i++)
        {
            if (RaccoonExist[i])
                RC[i].GetComponent<RaccoonController>().StartWork();
        }
    }

    public void StopRCWork()
    {
        for (int i = 0; i < RaccoonCount; i++)
        {
            if (RaccoonExist[i])
                RC[i].GetComponent<RaccoonController>().StopWork();
        }
    }

    public int RetCost(int RcIndex, int UpgradeIndex)
    {
        if (UpgradeIndex >= 0 && UpgradeIndex < 5)
            return RC[RcIndex].GetComponent<RaccoonController>().Cost[UpgradeIndex];
        return -1;
    }
}
