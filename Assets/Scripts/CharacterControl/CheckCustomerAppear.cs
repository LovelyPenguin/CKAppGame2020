using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCustomerAppear : MonoBehaviour
{
    public int condition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckAppear()
    {
        Debug.Log(PlayerPrefs.GetInt("CUSTOMERCOUNT"));
        if (condition <= PlayerPrefs.GetInt("CUSTOMERCOUNT"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
