using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayTestIn2D : MonoBehaviour
{
    public Customer[] ITEMS = new Customer[2];
    public Customer[] cus = new Customer[2];

    public bool[] iiiiitems = new bool[6];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            ITEMS[i] = cus[i];
        }

        int count = 0;
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                iiiiitems[count++] = cus[i].itemActive[j];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
