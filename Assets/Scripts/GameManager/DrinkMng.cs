using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private GameObject tabBtn;
    [SerializeField]
    private Text percentText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMiniGameData(int tabCountData, GameObject juiceData)
    {
        this.tabCountData = tabCountData;
        currentJuice = juiceData;
        if (previousJuiceData != null)
        {
            previousJuiceData.SetActive(false);
        }

        SetInvisibleButton();

        currentJuice.SetActive(true);

        PercentTextInitialize();

        previousJuiceData = juiceData;
    }

    public void MakeDrink()
    {
        if (currentJuice != null)
        {
            isStart = true;
            drinkMakeBtn.SetActive(false);
            tabBtn.SetActive(true);
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

    private void SetInvisibleButton()
    {
        if (currentJuice.GetComponent<JuiceObject>().isComplete == true)
        {
            drinkMakeBtn.SetActive(false);
        }
        else
        {
            drinkMakeBtn.SetActive(true);
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
}
