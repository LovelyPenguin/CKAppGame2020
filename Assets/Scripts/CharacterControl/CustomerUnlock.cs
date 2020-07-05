using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerUnlock : MonoBehaviour
{
    public JuiceData[] jui;
    public RaccoonController[] rac;

    private bool juiUnlock = false;
    private bool racUnlock = false;

    private bool debugJui;
    private bool debugRac;

    private Customer cus;

    // Start is called before the first frame update
    void Start()
    {
        if (jui.Length == 0)
        {
            juiUnlock = true;
        }
        else
        {
            juiUnlock = false;
        }

        if (rac.Length == 0)
        {
            racUnlock = true;
        }
        else
        {
            racUnlock = false;
        }

        cus = GetComponent<Customer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!cus.unlock)
        {
            cus.unlock = TotalUnlockCheck();
        }
    }

    public bool TotalUnlockCheck()
    {
        if (RacUnlockCheck() && JuiUnlockCheck())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool RacUnlockCheck()
    {
        bool bol = false;

        if (racUnlock)
        {
            return true;
            debugRac = true;
        }
        else
        {
            for (int i = 0; i < rac.Length; i++)
            {
                if (rac[i].GetRCState != RaccoonController.State.unActive)
                {
                    bol = true;
                    debugRac = true;
                }
                else
                {
                    bol = false;
                    debugRac = false;
                }
            }
        }
        return bol;
    }

    private bool JuiUnlockCheck()
    {
        bool bol = false;

        if (juiUnlock)
        {
            return true;
            debugJui = true;
        }
        else
        {
            for (int i = 0; i < rac.Length; i++)
            {
                if (jui[i].isUnlock)
                {
                    bol = true;
                    debugJui = true;
                }
                else
                {
                    bol = false;
                    debugJui = false;
                }
            }
        }
        return bol;
    }
}
