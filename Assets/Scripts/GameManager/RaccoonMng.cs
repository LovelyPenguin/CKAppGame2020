using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaccoonMng : MonoBehaviour
{
    private int selectedRC;
    private static int RaccoonCount = 10;
    public GameObject[] RC = new GameObject[RaccoonCount];
    GameObject[] Raccoon = new GameObject[RaccoonCount];
    bool[] RaccoonExist = new bool[RaccoonCount];
    public bool[] RaccoonUnlock = new bool[RaccoonCount];
    int[] RaccoonRank = new int[RaccoonCount];
    private bool isCameraChanged;
    GameMng GMng;

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
            if(!isCameraChanged)
            {
                isCameraChanged = true;
                Camera.main.GetComponent<CameraController>().RememberPos();
            }
            Camera.main.GetComponent<CameraController>().MoveScreenEdge();
            
        }
        else
        {
            isCameraChanged = false;
        }
    }

    public void GenerateRaccoon()
    {
        if (!GameObject.Find("GameManager").GetComponent<GameMng>().getOpenData)
        {
            selectedRC = GameObject.Find("RCinfo").GetComponent<SetRCInfo>().GetCurRCChoice;

            Debug.Log(selectedRC); 
            
            if (!RaccoonExist[selectedRC])
            {
                RC[selectedRC].transform.position = new Vector3(5, 1, 5);
                RC[selectedRC].GetComponent<RaccoonController>().SetRCActive(true);
                RaccoonExist[selectedRC] = true;
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

    public void StartRCWork()
    {
        for(int i =0;i<RaccoonCount; i++)
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
        if (RaccoonRank[index] < 5 && GMng.money >= GetRCCost(index, RaccoonRank[index]))
        {
            GMng.money -= GetRCCost(index, RaccoonRank[index]);
            RaccoonRank[index]++;
        }
    }

    public void UnlockRC(int index)
    {
        if (GMng.money >= GetRCCost(index, 0))
        {
            GMng.money -= GetRCCost(index, 0);
            RaccoonUnlock[index] = true;
            RaccoonRank[index] = 1;
            GenerateRaccoon();
        }
    }

    public int GetRCCost(int RCnum, int UPGnum)
    {
        return RC[RCnum].GetComponent<RaccoonController>().Cost[UPGnum];
    }
}
