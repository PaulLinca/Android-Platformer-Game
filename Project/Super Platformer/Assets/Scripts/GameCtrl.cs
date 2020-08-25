using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCtrl : MonoBehaviour
{
    public static GameCtrl instance;
    public float restartDelay;
    public GameData data;
    string dataFilePath;
    BinaryFormatter binaryFormatter;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        binaryFormatter = new BinaryFormatter();
        dataFilePath = Application.persistentDataPath + "/game.dat";

        Debug.Log(dataFilePath);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ResetData();
        }
    }

    public void SaveData()
    {
        FileStream fs = new FileStream(dataFilePath, FileMode.Create);
        binaryFormatter.Serialize(fs, data);
        fs.Close();
    }

    public void LoadData()
    {
        if(File.Exists(dataFilePath))
        {
            FileStream fs = new FileStream(dataFilePath, FileMode.Open);
            if (fs.Length != 0)
            {
                data = (GameData) binaryFormatter.Deserialize(fs);
                Debug.Log("Number of coins = " + data.coinCount);
            }
            fs.Close();
        }
    }
   
    void OnEnable()
    {
        Debug.Log("Data Loaded");
        LoadData();
    }   

    void OnDisable()
    {
        Debug.Log("Data Saved");
        SaveData();
    }

    void ResetData()
    {
        FileStream fs = new FileStream(dataFilePath, FileMode.Create);
        data.coinCount = 0;
        binaryFormatter.Serialize(fs, data);
        fs.Close();
    }

    //restarts the level
    public void PlayerDied(GameObject player)
    {
        player.SetActive(false);

        Invoke("RestartLevel", restartDelay);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
