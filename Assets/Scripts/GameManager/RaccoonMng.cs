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
    public bool[] RaccoonUnlock = new bool[RaccoonCount];
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
        if(isRCOnDrag())
        {
            Camera.main.GetComponent<CameraController>().MoveScreenEdge();
        }
    }

    public void GenerateRaccoon()
    {
        if (!GameObject.Find("GameManager").GetComponent<GameMng>().getOpenData)
        {
            selectedRC = GameObject.Find("RaccoonPreview").GetComponent<SetRCInfo>().GetCurRCChoice;

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
