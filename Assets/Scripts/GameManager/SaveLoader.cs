using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class SaveLoader : MonoBehaviour
{
    SavedFilesData defsave = new SavedFilesData();

    // Start is called before the first frame update
    void Start()
    {
        if (!LoadData<SavedFilesData>(ref defsave, "def"))
        {
            defsave.FileNum = 0;
            SaveDef();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SaveDef()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/def.dat");

        bf.Serialize(file, defsave);
        file.Close();

        Debug.Log("SaveDef success");
    }

    
    // T 형 데이터에 대한 템플릿 함수로 선언되어 있으며 T에는 시리얼라이즈된 클래스를 넘겨주어야 한다.
    public bool SaveData<T>(ref T Data, string FileName)
    {
        if (defsave.FileNum < 10)
        {
            if (!CheckFileExist(FileName))
            {
                defsave.FileNames[defsave.FileNum] = FileName;
                defsave.FileNum++;
                SaveDef();
            }

            string fileName = "/" + FileName + ".dat";
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + fileName);

            bf.Serialize(file, Data);
            file.Close();

            Debug.Log(FileName + " Saved!");

            

            return true;
        }
        else
            return false;
    }

    // T 형 데이터에 대한 템플릿 함수로 선언되어 있으며 T에는 시리얼라이즈된 클래스를 넘겨주어야 한다.
    public bool LoadData<T>(ref T Data, string FileName)
    {
        string fileName = "/" + FileName + ".dat";
        if (File.Exists(Application.persistentDataPath + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
            Data = (T)bf.Deserialize(file);
            file.Close();

            Debug.Log(FileName + " Loaded!");
            return true;
        }
        else

            Debug.Log(FileName + " Load Failed!");
            return false;
    }

    public bool CheckFileExist(string FileName)
    {
        return File.Exists(Application.persistentDataPath + "/" + FileName + ".dat");
    }

    public void ResetData()
    {
        for (int i = 0; i < defsave.FileNum; i++)
        {
            if (CheckFileExist(defsave.FileNames[i]))
                File.Delete(Application.persistentDataPath + "/" + defsave.FileNames[i] + ".dat");
        }
        defsave.FileNum = 0;
        PlayerPrefs.DeleteAll();
        SaveDef();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Debug.Log("Game Saved Data Reseted!");
    }
    
}

[Serializable]
class SavedFilesData
{
    public string[] FileNames = new string[10];
    public int FileNum;
}