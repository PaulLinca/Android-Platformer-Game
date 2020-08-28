using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCtrl : MonoBehaviour
{
    public static GameCtrl instance;
    public float restartDelay;
    public GameData data;
    string dataFilePath;
    BinaryFormatter binaryFormatter;

    public int coinScoreValue;

    public UI ui;

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
                ui.textCoinCount.text = $" x {data.coinCount}";
                ui.textScore.text = $"Score: {data.score}";
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
        data.score = 0;
        ui.textCoinCount.text = $" x {data.coinCount}";
        ui.textScore.text = $"Score: {data.score}";

        for(int key = 0; key < 3; key++)
        {
            data.keyFound[key] = false;
        }

        binaryFormatter.Serialize(fs, data);
        fs.Close();
    }

    //restarts the level
    public void PlayerDied(GameObject player)
    {
        player.SetActive(false);

        Invoke("RestartLevel", restartDelay);
    }

    public void UpdateCoinCount()
    {
        data.coinCount++;
        ui.textCoinCount.text = $" x {data.coinCount}";

        UpdateScore(coinScoreValue);
    }

    public void UpdateScore(int scoreIncrement)
    {
        data.score += scoreIncrement;
        ui.textScore.text = $"Score: {data.score}";
    }

    public void UpdateKeyCount(int keyNumber)
    {
        data.keyFound[keyNumber] = true;
        if(keyNumber == 0)
        {
            ui.key0.sprite = ui.key0Full;
        }
        else if(keyNumber == 1)
        {
            ui.key1.sprite = ui.key1Full;
        }
        else if(keyNumber == 2)
        {
            ui.key2.sprite = ui.key2Full;
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
