using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Networking;

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
        CheckDb();
    }

    public bool IsLevelUnlocked(int levelNumber)
    {
        return data.levelData[levelNumber].isUnlocked;
    }

    public int GetLevelStarsAwarded(int levelNumber)
    {
        return data.levelData[levelNumber].starsAwarded;
    }

    void CheckDb()
    {
        if(!File.Exists(dataFilePath))
        {
            #if UNITY_ANDROID && !UNITY_EDITOR

                var srcFile = System.IO.Path.Combine(Application.streamingAssetsPath, "game.dat");
                var downloader = UnityWebRequest.Get(srcFile);
                while(!downloader.isDone)
                {
                    //nothing
                }

                File.WriteAllBytes(dataFilePath, downloader.downloadHandler.data);
                RefreshData();

            #endif
        }    
        else
        {
            RefreshData();
        }
    }
}
