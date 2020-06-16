using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AddTimeToPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private bool isPress;
    [SerializeField]
    private float timer;
    [SerializeField]
    private float totalTime;
    public int count;
    public float coolTimer;
    public int plusCount = 1;
    public SetOpenTimer timerObj;
    public bool isAdd;

    // Start is called before the first frame update
    void Start()
    {
        isPress = false;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPress)
        {
            timer += Time.deltaTime;
            totalTime += Time.deltaTime;
            if (timer >= coolTimer)
            {
                count += plusCount;
                if (isAdd)
                {
                    timerObj.PlusTime(count);
                }
                else
                {
                    timerObj.MinusTime(ref count);
                }
                timer = 0f;
            }
            if (totalTime >= 0.3f)
            {
                plusCount++;
                totalTime = 0f;
            }
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        isPress = true;
        Debug.Log("DOWN");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPress = false;
        Debug.Log("Up");
        timer = 0f;
        totalTime = 0f;
        plusCount = 1;
        count = 0;
    }
}
