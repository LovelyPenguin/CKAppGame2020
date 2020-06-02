using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealMapMng : MonoBehaviour
{
    public GameObject[] Maps = new GameObject[4];
    public GameObject HealMapUnlockUI;
    private int enabledMapCount = 0;
    public int DefaultMapUnlockCost = 500000;
    public float ProductionRatio = 1.6f;
    GameMng GMng;
    // Start is called before the first frame update
    void Start()
    {
        GMng = GameObject.Find("GameManager").GetComponent<GameMng>();
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

    private bool HealUnlockUIActive = false;

    int cost;
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

        if(enabledMapCount != 0)
        {
            cost = (int)(DefaultMapUnlockCost * Mathf.Pow(ProductionRatio, enabledMapCount - 1));
            HealMapUnlockUI.GetComponentsInChildren<Text>()[0].text = "휴식공간을 해금하시겠습니까?\n비용 = " + cost.ToString();
        }
        else
        {
            cost = 0;
            HealMapUnlockUI.GetComponentsInChildren<Text>()[0].text = "휴식공간을 해금하시겠습니까?";
        }

        HealUnlockUIActive = true;
        Debug.Log("Popup");
    }

    public void UnlockHealMap()
    {
        if (GMng.money >= cost)
        {
            GMng.money -= cost;
            Maps[selectedMap].GetComponent<HealMapData>().Enable = true;
            enabledMapCount++;
            HealUnlockUIActive = false;
            HealMapUnlockUI.SetActive(false);
        }
    }

    public void UnlockCancel()
    {
        HealUnlockUIActive = false;
        HealMapUnlockUI.SetActive(false);
    }

    private void LateUpdate()
    {
        if (HealUnlockUIActive && !GMng.isPopupMenuOpen)
            HealMapUnlockUI.SetActive(true);
        else
        {
            HealMapUnlockUI.SetActive(false);
            HealUnlockUIActive = false;
        }
    }
}
