using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorStatMng : MonoBehaviour
{
    public static int count = 1;
    public Material Floor2Mtrl;
    public Material[] Floor2ObjMtrls = new Material[count];
    public GameObject CollisionGround2;

    private bool floor2enable;
    public bool SecondFloorStat
    {
        get { return floor2enable; }
    }   
    public enum Floor { Floor1 = 0, Floor2 };
    public Floor CurFloor
    {
        get { return curFloor; }
    }
    private Floor curFloor;
    // Start is called before the first frame update
    void Start()
    {
        floor2enable = false;
        SetFloor1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFloor1()
    {
        Floor2Mtrl.color = new Color(Floor2Mtrl.color.r, Floor2Mtrl.color.g, Floor2Mtrl.color.b, 0f);
        foreach (Material m in Floor2ObjMtrls)
            m.color = new Color(m.color.r, m.color.g, m.color.b, 0f);
        curFloor = Floor.Floor1;
        CollisionGround2.SetActive(false);
    }

    public void SetFloor2()
    {
        if (floor2enable)
        {
            Floor2Mtrl.color = new Color(Floor2Mtrl.color.r, Floor2Mtrl.color.g, Floor2Mtrl.color.b, 1f);
            foreach (Material m in Floor2ObjMtrls)
                m.color = new Color(m.color.r, m.color.g, m.color.b, 1f);
            curFloor = Floor.Floor2;
            CollisionGround2.SetActive(true);
        }
    }

    public void UnlockSecondFloor()
    {
        floor2enable = true;
    }
}
