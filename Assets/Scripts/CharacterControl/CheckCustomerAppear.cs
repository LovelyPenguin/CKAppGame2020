using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCustomerAppear : MonoBehaviour
{
    public int condition;
    public RaccoonController[] rac;

    private bool racOption;

    // Start is called before the first frame update
    void Start()
    {
        if (rac.Length == 0)
        {
            racOption = true;
        }
        else
        {
            racOption = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckAppear()
    {
        if (!racOption)
        {
            return NoRac();
        }
        else
        {
            return Rac();
        }
    }

    private bool NoRac()
    {
        if (condition <= PlayerPrefs.GetInt("CUSTOMERCOUNT"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool Rac()
    {
        bool returnValue = false;
        for (int i = 0; i < rac.Length; i++)
        {
            if (condition <= PlayerPrefs.GetInt("CUSTOMERCOUNT") && (rac[i].GetRCState == RaccoonController.State.inMap1 || rac[i].GetRCState == RaccoonController.State.inMap2))
            {
                returnValue = true;
            }
            else if (condition > PlayerPrefs.GetInt("CUSTOMERCOUNT") && (rac[i].GetRCState == RaccoonController.State.inMap1 || rac[i].GetRCState == RaccoonController.State.inMap2))
            {
                returnValue = false;
            }
        }
        return returnValue;
    }
}
