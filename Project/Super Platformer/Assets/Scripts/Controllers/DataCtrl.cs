using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataCtrl : MonoBehaviour
{
    public static DataCtrl instance = null;

    public GameData data;

    string dataFilePath;
    BinaryFormatter binaryFormatter;

    void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        binaryFormatter = new BinaryFormatter();

        dataFilePath = Application.persistentDataPath + "/game.dat";

        Debug.Log(dataFilePath);
    }
    
    public void RefreshData()
    {
        if(File.Exists(dataFilePath))
        {
            FileStream fileStream = new FileStream(dataFilePath, FileMode.Open);
            data = (GameData) binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            
            Debug.Log("Data refreshed");
        }
    }

    public void SaveData()
    {
        FileStream fileStream = new FileStream(dataFilePath, FileMode.Create);
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    
        Debug.Log("Data saved");
    }

    void OnEnable()
    {
        RefreshData();
    }
}
