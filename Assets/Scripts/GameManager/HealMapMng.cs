﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealMapMng : MonoBehaviour
{
    public GameObject[] Maps = new GameObject[4];
    public GameObject HealMapUnlockUI;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int retSeatIndexForName(string name)
    {
        int MapIndex;
        if(name == Maps[0].name)
        {
            MapIndex = 0;
        }
        else if(name == Maps[1].name)
        {
            MapIndex = 1;
        }
        else if (name == Maps[2].name)
        {
            MapIndex = 2;
        }
        else if (name == Maps[3].name)
        {
            MapIndex = 3;
        }
        else
        {
            return -1;
        }

        return Maps[MapIndex].GetComponent<HealMapData>().retSeatIndex();
    }

    public Vector3 retPositionForName(int index, string name)
    {
        int MapIndex;
        if (name == Maps[0].name)
        {
            MapIndex = 0;
        }
        else if (name == Maps[1].name)
        {
            MapIndex = 1;
        }
        else if (name == Maps[2].name)
        {
            MapIndex = 2;
        }
        else if (name == Maps[3].name)
        {
            MapIndex = 3;
        }
        else
        {
            MapIndex = 0;
        }
        return Maps[MapIndex].GetComponent<HealMapData>().retPosition(index);
    }

    public void releaseSeatForName(int index, string name)
    {
        int MapIndex;
        if (name == Maps[0].name)
        {
            MapIndex = 0;
        }
        else if (name == Maps[1].name)
        {
            MapIndex = 1;
        }
        else if (name == Maps[2].name)
        {
            MapIndex = 2;
        }
        else if (name == Maps[3].name)
        {
            MapIndex = 3;
        }
        else
        {
            MapIndex = 0;
        }

        Maps[MapIndex].GetComponent<HealMapData>().releaseSeat(index);
    }

    public int retSeatCountForName(string name)
    {
        int MapIndex;
        if (name == Maps[0].name)
        {
            MapIndex = 0;
        }
        else if (name == Maps[1].name)
        {
            MapIndex = 1;
        }
        else if (name == Maps[2].name)
        {
            MapIndex = 2;
        }
        else if (name == Maps[3].name)
        {
            MapIndex = 3;
        }
        else
        {
            MapIndex = 0;
        }

        return Maps[MapIndex].GetComponent<HealMapData>().retSeatCount();
    }

    private int selectedMap;

    public void FindMapIndex(string name)
    {
        if (name == Maps[0].name)
        {
            selectedMap = 0;
        }
        else if (name == Maps[1].name)
        {
            selectedMap = 1;
        }
        else if (name == Maps[2].name)
        {
            selectedMap = 2;
        }
        else if (name == Maps[3].name)
        {
            selectedMap = 3;
        }
        else
        {
            selectedMap = 0;
        }

        HealMapUnlockUI.SetActive(true);
        Debug.Log("Popup");
    }

    public void UnlockHealMap()
    {
        Maps[selectedMap].GetComponent<HealMapData>().Enable = true;
        HealMapUnlockUI.SetActive(false);
    }

    public void UnlockCancel()
    {
        HealMapUnlockUI.SetActive(false);
    }
}
