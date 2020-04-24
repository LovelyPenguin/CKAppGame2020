using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimingSlider : MonoBehaviour
{
    public Slider mySlider;
    public bool isLeft = false;
    public bool isClick = false;
    // Start is called before the first frame update
    void Start()
    {
        mySlider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        PercentCalculator();
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
            mySlider.value -= 1 * Time.deltaTime;
        }
        else if (!isLeft && !isClick)
        {
            mySlider.value += 1 * Time.deltaTime;
        }
    }

    void PercentCalculator()
    {
        if (isClick)
        {
            if (mySlider.value > 1)
            {
                Debug.Log(100 * (mySlider.maxValue - mySlider.value));
            }
            else if (mySlider.value <= 1)
            {
                Debug.Log(100 * mySlider.value);
            }
        }
    }
}
