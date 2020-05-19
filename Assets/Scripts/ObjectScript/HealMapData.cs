using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealMapData : MonoBehaviour
{
    public Vector3[] Locations = new Vector3[3];
    private bool[] Usable = new bool[3];
    public bool Enable = true;
    private int SeatCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
            Usable[i] = true;
        Enable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int retSeatIndex()
    {
        if (Enable)
        {
            for (int i = 0; i < 3; i++)
            {
                if (Usable[i])
                {
                    Usable[i] = false;
                    SeatCount++;
                    return i;
                }
            }
        }
        return -1;
    }
    public Vector3 retPosition(int index)
    {
        return Locations[index] + this.transform.position;
    }

    public void releaseSeat(int index)
    {
        Usable[index] = true;
        SeatCount--;
    }

    public int retSeatCount()
    {
        return SeatCount;
    }

}
