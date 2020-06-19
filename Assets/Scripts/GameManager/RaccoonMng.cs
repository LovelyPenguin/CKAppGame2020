using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Assertions.Must;


[Serializable]
class SaveData
{
    public bool[] RCEXIST = new bool[7];
    public bool[] RCUNLOCK = new bool[7];
    public int[] RCRANK = new int[7];
}


public class RaccoonMng : MonoBehaviour
{
    GameMng GMng;
    private int selectedRC;
    private static int RaccoonCount = 7;
    private static int RaccoonRankCount = 3;
    public GameObject[] RC = new GameObject[RaccoonCount];
    bool[] RaccoonExist = new bool[RaccoonCount];
    public bool[] RaccoonUnlock = new bool[RaccoonCount];
    int[] RaccoonRank = new int[RaccoonCount];
    public float[,] RCEfficiency = new float[RaccoonCount, RaccoonRankCount];

    public int MaxRCCountperMap = 3;
    private int map1RCCount = 0;
    private int map2RCCount = 0;

    


    /*
     * 라쿤의 장사 금액 효율을 반환한다
     * ex) 1.0f, 1.5f
     */
    public float GetRCEfficiency(int RCIndex, int RankIndex)
    {
        if (RCIndex < 0 && RCIndex >= RaccoonCount && RankIndex < 0 && RankIndex >= RaccoonRankCount)
            return 1f;
        return RCEfficiency[RCIndex, RankIndex];
    }

    // Start is called before the first frame update
    void Start()
    {
        GMng = GameObject.Find("GameManager").GetComponent<GameMng>();
        //UnlockRC(0);
        if (GMng.GetComponent<SaveLoader>().CheckFileExist("RCMNG"))
        {
            LoadData();
            for (int i = 0; i < 7; i++)
            {
                Debug.Log("RCUnlock Status = " + i + RaccoonUnlock[i]);
                if (RaccoonUnlock[i])
                    RC[i].GetComponent<RaccoonController>().SetRCActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isRCOnDrag())
        {
            Camera.main.GetComponent<CameraController>().MoveScreenEdge();
        }
    }

    public void SaveData()
    {
        SaveData save = new SaveData();
        save.RCEXIST = RaccoonExist;
        save.RCRANK = RaccoonRank;
        save.RCUNLOCK = RaccoonUnlock;

        GMng.GetComponent<SaveLoader>().SaveGame<SaveData>(ref save, "RCMNG");
    }

    public void LoadData()
    {
        SaveData save = new SaveData();
        GMng.GetComponent<SaveLoader>().LoadGame<SaveData>(ref save, "RCMNG");

        Array.Copy(save.RCEXIST, RaccoonExist, 7);
        Array.Copy(save.RCRANK, RaccoonRank, 7);
        Array.Copy(save.RCUNLOCK, RaccoonUnlock, 7);
    }

    public void GenerateRaccoon(int RCindex)
    {
        if (!GameObject.Find("GameManager").GetComponent<GameMng>().getOpenData)
        {
            Debug.Log(RCindex);

            if (!RaccoonExist[RCindex])
            {
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

    public int GetRCMaxRank()
    {
        return RaccoonRankCount;
    }

    public int GetMaxRCcount()
    {
        return RaccoonCount;
    }

    public void UpgradeRC(int index)
    {
        int cost = RetCost(index, RaccoonRank[index]);
        if (RaccoonRank[index] < RaccoonRankCount && RaccoonUnlock[index] && GMng.money >= cost && !GameObject.Find("GameManager").GetComponent<GameMng>().getOpenData)
        {
            RaccoonRank[index]++;
            RC[index].GetComponent<RaccoonController>().CallUpgradeTrigger();
            GMng.money -= cost;
        }
    }

    public void UnlockRC(int index)
    {
        if (!GMng.gameObject.GetComponent<FloorStatMng>().SecondFloorStat && map1RCCount == MaxRCCountperMap)
        {

            return;
        }
        int cost = RetCost(index, 0);
        if (!RaccoonUnlock[index] && GMng.money >= cost && !GameObject.Find("GameManager").GetComponent<GameMng>().getOpenData)
        {
            RaccoonUnlock[index] = true;
            RaccoonRank[index] = 1;
            GMng.money -= cost;
            GenerateRaccoon(index);
            if (index == 4)
            {
                GameObject.Find("GameManager").GetComponent<FloorStatMng>().UnlockSecondFloor();
                Debug.Log("2nd Floor Unlocked");
            }

        }
    }

    public void StartRCWork()
    {
        for (int i = 0; i < RaccoonCount; i++)
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
        if (UpgradeIndex >= 0 && UpgradeIndex < RaccoonRankCount)
            return RC[RcIndex].GetComponent<RaccoonController>().Cost[UpgradeIndex];
        return -1;
    }

    public bool MoveToAnotherFloor(int floor)
    {
        switch (floor)
        {
            case 1:
                if (map1RCCount == MaxRCCountperMap)
                    return false;
                else
                {
                    map1RCCount++;
                    return true;
                }
            case 2:
                if (GameObject.Find("GameManager").GetComponent<FloorStatMng>().SecondFloorStat)
                {
                    if (map2RCCount == MaxRCCountperMap)
                        return false;
                    else
                    {
                        map2RCCount++;
                        return true;
                    }
                }
                else
                    return false;

        }
        return false;
    }

    public bool CanMoveToAnotherFloor(int floor)
    {
        switch(floor)
        {
            case 1:
                return map1RCCount != MaxRCCountperMap;
            case 2:
                return map2RCCount != MaxRCCountperMap;
        }
        return false;
    }
    public void ReleaseMapCount(int floor)
    {
        if(floor == 1)
            map1RCCount--;
        if (floor == 2)
            map2RCCount--;
    }
}

