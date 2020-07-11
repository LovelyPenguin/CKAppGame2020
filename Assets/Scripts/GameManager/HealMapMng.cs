using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
class HSaveData
{
    public bool[] HMAPUNLOCK = new bool[4];
    public int ENABLEDMAPCOUNT;
    public bool[] MAP0USABLE = new bool[3];
    public bool[] MAP1USABLE = new bool[3];
    public bool[] MAP2USABLE = new bool[3];
    public bool[] MAP3USABLE = new bool[3];
}

public class HealMapMng : MonoBehaviour
{
    public int HealMapCount = 4;
    public GameObject[] Maps;
    public GameObject HealMapUnlockUI;
    private int enabledMapCount = 0;
    public int DefaultMapUnlockCost = 500000;
    public float ProductionRatio = 1.6f;
    public bool loadFail = false;
    // Start is called before the first frame update

    private static HealMapMng instance;
    public static HealMapMng Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HealMapMng>();
            }

            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SaveData()
    {
        HSaveData save = new HSaveData();
        for(int i=0;i< HealMapCount; i++)
            save.HMAPUNLOCK[i] = Maps[i].GetComponent<HealMapData>().Enable;
        save.ENABLEDMAPCOUNT = enabledMapCount;
        Array.Copy(Maps[0].GetComponent<HealMapData>().Usable, save.MAP0USABLE, 3);
        Array.Copy(Maps[1].GetComponent<HealMapData>().Usable, save.MAP1USABLE, 3);
        Array.Copy(Maps[2].GetComponent<HealMapData>().Usable, save.MAP2USABLE, 3);
        Array.Copy(Maps[3].GetComponent<HealMapData>().Usable, save.MAP3USABLE, 3);

        GameMng.Instance.gameObject.GetComponent<SaveLoader>().SaveData<HSaveData>(ref save,"HMNG");
    }

    public void LoadData()
    {
        HSaveData save = new HSaveData();
        if (GameMng.Instance.gameObject.GetComponent<SaveLoader>().LoadData<HSaveData>(ref save, "HMNG"))
        {
            for (int i = 0; i < HealMapCount; i++)
            {
                Maps[i].GetComponent<HealMapData>().Enable = save.HMAPUNLOCK[i];
                if (Maps[i].GetComponent<HealMapData>().Enable)
                    Maps[i].GetComponent<HealMapData>().UnlockAnimStart();
            }
            Array.Copy(save.MAP0USABLE, Maps[0].GetComponent<HealMapData>().Usable, 3);
            Array.Copy(save.MAP1USABLE, Maps[1].GetComponent<HealMapData>().Usable, 3);
            Array.Copy(save.MAP2USABLE, Maps[2].GetComponent<HealMapData>().Usable, 3);
            Array.Copy(save.MAP3USABLE, Maps[3].GetComponent<HealMapData>().Usable, 3);
            enabledMapCount = save.ENABLEDMAPCOUNT;
        }
        loadFail = true;
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

        if (selectedMap == 1 || selectedMap == 0)
        {
            if (enabledMapCount != 0)
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
        }
        else
        {
            GameMng.Instance.gameObject.GetComponent<FailMsgBox>().Create("이 휴식공간은 아직 사용 할수 없어요!");
        }
        Debug.Log("Popup");
    }

    public void UnlockHealMap()
    {
        if (GameMng.Instance.money >= cost)
        {
            GameMng.Instance.money -= cost;
            Maps[selectedMap].GetComponent<HealMapData>().Enable = true;
            Maps[selectedMap].GetComponent<HealMapData>().UnlockAnimStart();
            Maps[selectedMap].GetComponent<HealMapData>().SetAllUsableTrue();
            enabledMapCount++;
        }
        else
        {
            GameMng.Instance.gameObject.GetComponent<FailMsgBox>().Create();
        }
        HealUnlockUIActive = false;
        HealMapUnlockUI.SetActive(false);
    }

    public void UnlockCancel()
    {
        HealUnlockUIActive = false;
        HealMapUnlockUI.SetActive(false);
    }

    private void LateUpdate()
    {
        if (HealUnlockUIActive && !GameMng.Instance.isPopupMenuOpen)
            HealMapUnlockUI.SetActive(true);
        else
        {
            HealMapUnlockUI.SetActive(false);
            HealUnlockUIActive = false;
        }
    }
}
