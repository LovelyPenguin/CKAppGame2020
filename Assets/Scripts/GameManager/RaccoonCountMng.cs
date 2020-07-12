using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonCountMng : MonoBehaviour
{
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        counter = PlayerPrefs.GetInt("CUSTOMERCOUNT");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetRaccoonCount()
    {
        counter = 0;
        for (int i = 0; i < GetComponent<RaccoonMng>().RC.Length; i++)
        {
            if (GetComponent<RaccoonMng>().RC[i].GetComponent<RaccoonController>().GetRCState == RaccoonController.State.unActive ||
                GetComponent<RaccoonMng>().RC[i].GetComponent<RaccoonController>().GetRCState == RaccoonController.State.Healing)
            {
                //Debug.Log(GetComponent<RaccoonMng>().RC[i].GetComponent<RaccoonController>().GetRCState);
            }
            else
            {
                counter++;
            }
        }
        //PlayerPrefs.SetInt("CUSTOMERCOUNT", counter);
        //Debug.Log(counter);
        return counter;
    }
}
