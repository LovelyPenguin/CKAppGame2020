using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealMapData : MonoBehaviour
{
    public Vector3[] Locations = new Vector3[3];
    public bool[] Usable = new bool[3];
    public bool Enable = false;
    private int SeatCount = 0;

    // this is for test
    public GameObject Quad;
    //

    // Start is called before the first frame update
    void Start()
    {
    //    if (HealMapMng.Instance.loadFail)
    //        for (int i = 0; i < 3; i++)
    //            Usable[i] = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Quad.SetActive(!Enable);
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

    public void UnlockThis()
    {
        if (!Enable)
            HealMapMng.Instance.FindMapIndex(this.name);
    }

    public void UnlockAnimStart()
    {
        StartCoroutine(UnlockAnim());
    }

    public void SetAllUsableTrue()
    {
        for (int i = 0; i < 3; i++)
            Usable[i] = true;
    }

    IEnumerator UnlockAnim()
    {
        float t = 0.0f;
        Vector3 endScale = Quad.transform.localScale * 1.2f;
        
        while (t <= 1.0f)
        {
            Quad.transform.localScale = Vector3.Lerp(Quad.transform.localScale, endScale, t);
            t += Time.deltaTime * 4;
            yield return null;
        }
        Quad.transform.localScale = endScale;
        t = 0.0f;
        while (t <= 1.0f)
        {
            Quad.transform.localScale = Vector3.Lerp(Quad.transform.localScale, Vector3.zero, t);
            t += Time.deltaTime * 2;
            yield return null;
        }
        Quad.transform.localScale = Vector3.one;
        Quad.SetActive(false);
    }
}
