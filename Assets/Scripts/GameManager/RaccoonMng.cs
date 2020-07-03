using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.Experimental.GraphView;
//using UnityEditor.Tilemaps;
using UnityEngine;


[Serializable]
class RSaveData
{
    public bool[] RCEXIST = new bool[7];
    public bool[] RCUNLOCK = new bool[7];
    public int[] RCRANK = new int[7];
    public int[] RCSTAMINA = new int[7];
    public int CURRCCOUNT;
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
    private int curRCCount = 0;

    


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
                {
                    RC[i].GetComponent<RaccoonController>().SetRCActive(true);
                    RC[i].GetComponent<RaccoonController>().CallUpgradeTrigger(RaccoonRank[i]);
                    if (i == 4)
                    {
                        GameObject.Find("GameManager").GetComponent<FloorStatMng>().UnlockSecondFloor();
                        Debug.Log("2nd Floor Unlocked");
                    }
                }
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
        RSaveData save = new RSaveData();
        save.RCEXIST = RaccoonExist;
        save.RCRANK = RaccoonRank;
        save.RCUNLOCK = RaccoonUnlock;
        save.CURRCCOUNT = curRCCount;
        for (int i = 0; i < RaccoonCount; i++)
            save.RCSTAMINA[i] = RC[i].GetComponent<RaccoonController>().stamina;

        GMng.GetComponent<SaveLoader>().SaveData<RSaveData>(ref save, "RCMNG");
    }

    public void LoadData()
    {
        RSaveData save = new RSaveData();
        GMng.GetComponent<SaveLoader>().LoadData<RSaveData>(ref save, "RCMNG");

        Array.Copy(save.RCEXIST, RaccoonExist, 7);
        Array.Copy(save.RCRANK, RaccoonRank, 7);
        Array.Copy(save.RCUNLOCK, RaccoonUnlock, 7);
        curRCCount = save.CURRCCOUNT;
        for (int i = 0; i < RaccoonCount; i++)
            RC[i].GetComponent<RaccoonController>().stamina = save.RCSTAMINA[i];
    }

    public void GenerateRaccoon(int RCindex)
    {
        if (!GameObject.Find("GameManager").GetComponent<GameMng>().getOpenData)
        {
            Debug.Log(RCindex);

            if (!RaccoonExist[RCindex])
            {
                RC[RCindex].GetComponent<RaccoonController>().SetRCActive(true);
                RC[RCindex].GetComponent<RaccoonController>().stamina = 50;
                RaccoonExist[RCindex] = true;
                curRCCount++;
                GameObject.Find("CutSceneCanvas").GetComponent<CutSceneControl>().CutSceneStart(RCindex);
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

    public void UpgradeRC(int index, Sprite RCSprite)
    {
        int cost = RetCost(index, RaccoonRank[index]);
        if (GMng.money < cost)
        {
            GMng.gameObject.GetComponent<FailMsgBox>().Create();
        }
        else if (GameObject.Find("GameManager").GetComponent<GameMng>().getOpenData)
        {
            GMng.gameObject.GetComponent<FailMsgBox>().Create("장사 중에는 라쿤을 업그레이드 할수 없어요!");
        }
        else if (RaccoonRank[index] < RaccoonRankCount )
        {
            RaccoonRank[index]++;
            RC[index].GetComponent<RaccoonController>().CallUpgradeTrigger(RaccoonRank[index]);
            GameObject.Find("CutSceneCanvas").GetComponent<CutSceneControl>().CutSceneStart(7).GetComponent<UpgradePopUp>().RCUpgradePopUp(RCSprite, RaccoonRank[index]);
            GMng.money -= cost;
        }
    }

    public void UnlockRC(int index)
    {
        if (!GMng.gameObject.GetComponent<FloorStatMng>().SecondFloorStat && curRCCount == MaxRCCountperMap)
        {
            if (index != 4)
            {
                gameObject.GetComponent<FailMsgBox>().Create("1층이 가득찼어요!\n새로운 라쿤을 데려오면 좋은 일이 생길지도?");
                return;
            }
        }
        int cost = RetCost(index, 0);
        if(GameObject.Find("GameManager").GetComponent<GameMng>().getOpenData)
        {
            gameObject.GetComponent<FailMsgBox>().Create("장사 중에는 새로운 라쿤을 데려올수 없어요!");
        }
        else if(GMng.money < cost)
        {
            gameObject.GetComponent<FailMsgBox>().Create();
        }
        else if (!RaccoonUnlock[index])
        {
            if (index == 4)
            {
                GameObject.Find("GameManager").GetComponent<FloorStatMng>().UnlockSecondFloor();
                Debug.Log("2nd Floor Unlocked");
            }
            RaccoonUnlock[index] = true;
            RaccoonRank[index] = 1;
            GMng.money -= cost;
            GenerateRaccoon(index);

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

