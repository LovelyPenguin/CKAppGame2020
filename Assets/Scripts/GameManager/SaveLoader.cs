using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    // T 형 데이터에 대한 템플릿 함수로 선언되어 있으며 T에는 시리얼라이즈된 클래스를 넘겨주어야 한다.
    public bool SaveGame<T>(ref T Data, string FileName)
    {
        string fileName = "/" + FileName + ".dat";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + fileName);

        bf.Serialize(file, Data);
        file.Close();

        Debug.Log(FileName + "Saved!");

        return true;
    }

    // T 형 데이터에 대한 템플릿 함수로 선언되어 있으며 T에는 시리얼라이즈된 클래스를 넘겨주어야 한다.
    public bool LoadGame<T>(ref T Data, string FileName)
    {
        string fileName = "/" + FileName + ".dat";
        if (File.Exists(Application.persistentDataPath + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
            Data = (T)bf.Deserialize(file);
            file.Close();

            Debug.Log(FileName + "Loaded!");
            return true;
        }
        else

            Debug.Log(FileName + "Load Failed!");
            return false;
    }

    public bool CheckFileExist(string FileName)
    {
        return File.Exists(Application.persistentDataPath + "/" + FileName + ".dat");
    }

    public bool ResetGame()
    {
        return false;
    }
}
