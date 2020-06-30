using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
class DrinkSaveData
{
    public bool[] UNLOCK = new bool[3];
}

public class DrinkMng : MonoBehaviour
{
    private int tabCountData;
    private int tabCounter = 0;
    private bool isStartMiniGame = false;
    private bool timerOn = false;
    private bool slideGameIsStart = false;
    private GameObject previousJuiceData;
    private GameObject currentJuice;

    [SerializeField]
    private int phase = 0;
    [SerializeField]
    private bool isStart = false;
    [SerializeField]
    private GameObject timingBar;
    [SerializeField]
    private GameObject drinkMakeBtn;
    [SerializeField]
    private GameObject unlockBtn;
    [SerializeField]
    private GameObject tabBtn;
    [SerializeField]
    private Text percentText;
    [SerializeField]
    private GameObject blockImage;

    public JuiceData[] juiceList;
    public Sprite[] juiceSprites;

    // Start is called before the first frame update
    void Awake()
    {
        if (GameMng.Instance.GetComponent<SaveLoader>().CheckFileExist("DRINKMNG"))
        {
            LoadData();
        }
        
        string[] juiceNames = new string[juiceList.Length];
        for (int i = 0; i < juiceList.Length; i++)
        {
            juiceNames[i] = juiceList[i].name;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    int tabCountDataTemp;
    GameObject juiceDataTemp;
    [HideInInspector]
    public GameObject mainObjTemp;
    public void SetMiniGameData(int tabCountData, GameObject juiceData, ref bool unlock, GameObject mainObj)
    {
        this.tabCountData = tabCountData;
        currentJuice = juiceData;
        if (previousJuiceData != null)
        {
            previousJuiceData.SetActive(false);
        }

        SetInvisibleButton(unlock);

        tabCountDataTemp = tabCountData;
        juiceDataTemp = juiceData;
        mainObjTemp = mainObj;

        currentJuice.SetActive(true);

        PercentTextInitialize();

        previousJuiceData = juiceData;
    }

    public void MakeDrink()
    {
        if (currentJuice != null)
        {
            blockImage.SetActive(true);
            isStart = true;
            drinkMakeBtn.SetActive(false);
            tabBtn.SetActive(true);
        }
    }

    public void UnlockDrink()
    {
        if (GameMng.Instance.money - mainObjTemp.GetComponent<JuiceData>().unlockCost >= 0)
        {
            GameMng.Instance.money -= mainObjTemp.GetComponent<JuiceData>().unlockCost;
            mainObjTemp.GetComponent<JuiceData>().isUnlock = true;
            SetMiniGameData(tabCountDataTemp, juiceDataTemp, ref mainObjTemp.GetComponent<JuiceData>().isUnlock, mainObjTemp);
        }
    }

    public void UnlockDrink(int cost, ref bool unlock)
    {
        drinkMakeBtn.SetActive(false);
        unlockBtn.SetActive(true);
        if (GameMng.Instance.money - cost >= 0)
        {
            //unlock = true;
            //GameMng.Instance.money -= cost;
        }
        else
        {
            //unlock = false;
        }
    }

    public void TabPhase()
    {
        if (currentJuice != null)
        {
            isStartMiniGame = true;
            if (TabMiniGame())
            {
                if (timingBar.active == false)
                {
                    StartCoroutine(SliderActive());
                }

                timingBar.SetActive(true);
                currentJuice.GetComponent<JuiceObject>().SetDecoSprite(false);
                currentJuice.GetComponent<JuiceObject>().FillMainSprite(1);

                if (slideGameIsStart)
                {
                    timingBar.GetComponent<TimingSlider>().PercentConfirm();

                    currentJuice.GetComponent<JuiceObject>().isComplete = true;
                    currentJuice.GetComponent<JuiceObject>().percentValue = timingBar.GetComponent<TimingSlider>().getPercentValue;

                    if (timingBar.GetComponent<TimingSlider>().getPercentValue >= 50)
                    {
                        currentJuice.GetComponent<JuiceObject>().SetDecoSprite(true);
                    }
                    PercentTextInitialize();
                    MiniGameEnd();
                }
            }
            else
            {
                currentJuice.GetComponent<JuiceObject>().FillMainSprite();
            }
        }
    }

    private bool TabMiniGame()
    {
        tabCounter++;
        if (tabCounter >= 1)
        {
            timerOn = true;
            if (tabCounter >= tabCountData)
            {
                return true;
            }
        }
        return false;
    }

    private void PercentTextInitialize()
    {
        if (currentJuice.GetComponent<JuiceObject>().percentValue <= 0)
        {
            percentText.text = null;
        }
        else
        {
            if (Mathf.Round(currentJuice.GetComponent<JuiceObject>().percentValue) >= 80 &&
                Mathf.Round(currentJuice.GetComponent<JuiceObject>().percentValue) < 90)
            {
                Debug.Log("Over 80%");
            }
            else if (Mathf.Round(currentJuice.GetComponent<JuiceObject>().percentValue) >= 90)
            {
                Debug.Log("Over 90%");
            }

            percentText.text = Mathf.Round(currentJuice.GetComponent<JuiceObject>().percentValue).ToString() + "%";
        }
    }

    private void SetInvisibleButton(bool isUnlock = true)
    {
        if (isUnlock)
        {
            unlockBtn.SetActive(false);
            if (currentJuice.GetComponent<JuiceObject>().isComplete)
            {
                drinkMakeBtn.SetActive(false);
            }
            else
            {
                drinkMakeBtn.SetActive(true);
            }
        }
        else
        {
            unlockBtn.SetActive(true);
        }
    }

    private void MiniGameEnd()
    {
        timingBar.SetActive(false);
        tabBtn.SetActive(false);
        //currentJuice.GetComponent<JuiceObject>().SetDecoSprite(true);
        isStartMiniGame = false;
        timerOn = false;
        slideGameIsStart = false;
        tabCounter = 0;
        blockImage.SetActive(false);
        SetInvisibleButton();
    }

    private IEnumerator SliderActive()
    {
        yield return new WaitForSeconds(1f);
        slideGameIsStart = true;
    }

    // 나중에 음료 배달기능이 완성되면 사용할 것
    public void DeliveryDrink()
    {
        currentJuice.GetComponent<JuiceObject>().ResetAllValue();
    }

    public void UpdateContent()
    {
        percentText.text = null;
        drinkMakeBtn.SetActive(true);
    }

    public void SaveData()
    {
        DrinkSaveData save = new DrinkSaveData();
        bool[] unlock = new bool[juiceList.Length];

        for (int i = 0; i < juiceList.Length; i++)
        {
            unlock[i] = juiceList[i].isUnlock;
            Debug.Log(unlock[i]);
        }
        save.UNLOCK = unlock;

        GameMng.Instance.GetComponent<SaveLoader>().SaveData<DrinkSaveData>(ref save, "DRINKDATASAVE");
    }

    public void LoadData()
    {
        DrinkSaveData save = new DrinkSaveData();
        GameMng.Instance.GetComponent<SaveLoader>().LoadData<DrinkSaveData>(ref save, "DRINKDATASAVE");

        bool[] unlock = new bool[juiceList.Length];

        Array.Copy(save.UNLOCK, unlock, juiceList.Length);

        for (int i = 0; i < juiceList.Length; i++)
        {
            juiceList[i].isUnlock = unlock[i];
        }
    }
}
