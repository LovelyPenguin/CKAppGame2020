using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
class CustomerSaveData
{
    public bool[] UNLOCK = new bool[2];
    public int[] GETMONEY = new int[2];
    public int[] STAMP = new int[2];
    public bool[,] ITEMS = new bool[2, 3];
}

public class CustomerMng : MonoBehaviour
{
    public Customer[] customers;
    // Start is called before the first frame update
    void Start()
    {
        if (GameMng.Instance.GetComponent<SaveLoader>().CheckFileExist("CUSTOMERSAVE"))
        {
            Debug.Log("CUSTOMERSAVE Found");
            LoadData();

            for (int i = 0; i < customers.Length; i++)
            {
                Debug.Log(customers[i] + " : " + customers[i].unlock);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("Pause");
        }
        else
        {
            Debug.Log("Resume");
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Application Quit");
    }

    public void SaveData()
    {
        CustomerSaveData save = new CustomerSaveData();
        bool[] unlock = new bool[customers.Length];
        int[] money = new int[customers.Length];
        int[] stamp = new int[customers.Length];
        bool[,] item = new bool[customers.Length, 3];

        for (int i = 0; i < customers.Length; i++)
        {
            unlock[i] = customers[i].unlock;
            money[i] = customers[i].money;
            stamp[i] = customers[i].stamp;
            for (int j = 0; j < 3; j++)
            {
                item[i, j] = customers[i].itemActive[j];
            }
        }
        save.UNLOCK = unlock;
        save.GETMONEY = money;
        save.STAMP = stamp;
        save.ITEMS = item;

        GameMng.Instance.GetComponent<SaveLoader>().SaveData<CustomerSaveData>(ref save, "CUSTOMERSAVE");
    }

    public void LoadData()
    {
        CustomerSaveData save = new CustomerSaveData();
        GameMng.Instance.GetComponent<SaveLoader>().LoadData<CustomerSaveData>(ref save, "CUSTOMERSAVE");

        bool[] unlock = new bool[customers.Length];
        int[] money = new int[customers.Length];
        int[] stamp = new int[customers.Length];
        bool[, ] item = new bool[customers.Length, 3];

        Array.Copy(save.UNLOCK, unlock, customers.Length);
        Array.Copy(save.GETMONEY, money, customers.Length);
        Array.Copy(save.STAMP, stamp, customers.Length);

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                item[i, j] = save.ITEMS[i, j];
            }
        }

        for (int i = 0; i < customers.Length; i++)
        {
            customers[i].unlock = unlock[i];
            customers[i].money = money[i];
            customers[i].stamp = stamp[i];
            for (int j = 0; j < 3; j++)
            {
                customers[i].itemActive[j] = item[i, j];
            }
        }
    }
}
