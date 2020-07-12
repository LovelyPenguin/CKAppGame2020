using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatSystem : MonoBehaviour
{
    private int count1;
    private int count2;
    // Start is called before the first frame update
    void Start()
    {
        count1 = 0;
        count2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Count1Add()
    {
        count1++;
        if (count1 >= 4)
        {
            count1 = 0;
        }
    }
    public void Count2Add()
    {
        if (count1 == 3)
        {
            count2++;
            if (count2 == 3)
            {
                Debug.Log("Cheat Start");
                count1 = 0;
                count2 = 0;
                GameMng.Instance.money = int.MaxValue;
            }
        }
        else
        {
            count1 = 0;
            count2 = 0;
        }
    }
}
