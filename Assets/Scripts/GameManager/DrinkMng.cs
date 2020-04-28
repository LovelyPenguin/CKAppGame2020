using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        currentJuice.SetActive(true);

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
                if (slideGameIsStart)
                {
                    timingBar.GetComponent<TimingSlider>().PercentConfirm();
                    MiniGameEnd();
                }
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

    private void MiniGameEnd()
    {
        timingBar.SetActive(false);
        tabBtn.SetActive(false);
        drinkMakeBtn.SetActive(true);
        isStartMiniGame = false;
        timerOn = false;
        slideGameIsStart = false;
        tabCounter = 0;
    }

    private IEnumerator SliderActive()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("asdasd");
        slideGameIsStart = true;
    }
}
