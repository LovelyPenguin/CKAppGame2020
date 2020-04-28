using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimingSlider : MonoBehaviour
{
    public Slider mySlider;
    public bool isLeft = false;
    private bool isClick = false;
    [Range(1, 30)]
    public float sliderSpeed = 1f;
    [SerializeField]
    private Text percentText;
    private float percent;

    public float getPercentValue
    {
        get
        {
            return percent;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        mySlider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveSlider();
    }

    void MoveSlider()
    {
        if (mySlider.value >= mySlider.maxValue)
        {
            isLeft = true;
        }
        else if (mySlider.value <= mySlider.minValue)
        {
            isLeft = false;
        }

        if (isLeft && !isClick)
        {
            mySlider.value -= sliderSpeed * Time.deltaTime;
        }
        else if (!isLeft && !isClick)
        {
            mySlider.value += sliderSpeed * Time.deltaTime;
        }
    }

    void PercentCalculator()
    {
        if (isClick)
        {
            if (mySlider.value > 1)
            {
                percent = 100 * (mySlider.maxValue - mySlider.value);
                Debug.Log(percent);
            }
            else if (mySlider.value <= 1)
            {
                percent = 100 * mySlider.value;
                Debug.Log(percent);
            }
        }
    }

    public void PercentConfirm()
    {
        isClick = true;
        PercentCalculator();
        isClick = false;
        mySlider.value = 0;
        gameObject.SetActive(false);
    }
}
