using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealMapData : MonoBehaviour
{
    public Vector3[] Locations = new Vector3[3];
    private bool[] Usable = new bool[3];
    public bool Enable = true;
    private int SeatCount = 0;
    public GameObject HMng;

    // this is for test
    public GameObject Quad;
    //

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
            Usable[i] = true;
        Enable = false;
    }

    // Update is called once per frame
    void Update()
    {
        Quad.SetActive(!Enable);
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

    private void OnMouseDown()
    {
        if (!Enable)
            HMng.GetComponent<HealMapMng>().FindMapIndex(this.name);
    }
}
