using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameCtrl : MonoBehaviour
{
    public enum Item
    {
        Coin,
        BigCoin,
        Enemy
    }

    public static GameCtrl instance;
    public float restartDelay;
    public GameData data;
    string dataFilePath;
    BinaryFormatter binaryFormatter;
    float timeLeft;

    public int coinScoreValue;
    public int bigCoinScoreValue;
    public int enemyScoreValue;

    public float maxTime;
    public UI ui;
    public GameObject bigCoin;

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

    void Start()
    {
        timeLeft = maxTime;

        HandleFirstBoot();
        UpdateHearts();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ResetData();
        }

        if(timeLeft > 0)
        {
            UpdateTimer();
        }
    }

    void HandleFirstBoot()
    {
        if(data.isFirstBoot)
        {
            data.lives = 3;
            data.coinCount = 0;
            data.keyFound[0] = false;
            data.keyFound[1] = false;
            data.keyFound[2] = false;
            data.score = 0;
            data.isFirstBoot = false;
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

        data.lives = 3;
        UpdateHearts();

        binaryFormatter.Serialize(fs, data);
        fs.Close();
    }

    void UpdateHearts()
    {
        if(data.lives == 3)
        {
            ui.heart0.sprite = ui.heartFull;
            ui.heart1.sprite = ui.heartFull;
            ui.heart2.sprite = ui.heartFull;
        }
        if(data.lives == 2)
        {
            ui.heart0.sprite = ui.heartEmpty;
        }
        if(data.lives == 1)
        {
            ui.heart0.sprite = ui.heartEmpty;
            ui.heart1.sprite = ui.heartEmpty;
        }
    }

    void CheckLives()
    {
        int updatedLives = data.lives;
        updatedLives--;
        data.lives = updatedLives;

        if(data.lives == 0)
        {
            data.lives = 3;
            SaveData();
            
            Invoke("GameOver", restartDelay);
        }
        else
        {
            SaveData();
            Invoke("RestartLevel", restartDelay);
        }
    }

    //restarts the level
    public void PlayerDied(GameObject player)
    {
        player.SetActive(false);
        CheckLives();
        // Invoke("RestartLevel", restartDelay);
    }

    public void UpdateCoinCount()
    {
        data.coinCount++;
        ui.textCoinCount.text = $" x {data.coinCount}";

        UpdateScore(Item.Coin);
    }

    public void UpdateScore(Item item)
    {
        int scoreIncrement = 0;
        switch(item)
        {
            case Item.Coin:
                scoreIncrement = coinScoreValue;
                break;
            case Item.BigCoin:
                scoreIncrement = bigCoinScoreValue;
                break;
            case Item.Enemy:
                scoreIncrement = enemyScoreValue;
                break;
            default:
                break;
        }
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

    void UpdateTimer()
    {
        timeLeft -= Time.deltaTime;

        ui.textTimer.text =  $"Timer: {(int) timeLeft}";
        if(timeLeft <= 0)
        {
            ui.textTimer.text =  "Timer: 0";
            
            GameObject player = GameObject.FindGameObjectWithTag("Player") as GameObject;
            PlayerDied(player);
        }
    }

    void GameOver()
    {
        ui.panelGameOver.SetActive(true);
    }

    public void PlayerDiedAnimation(GameObject player)
    {
        // Throw player object back 
        var rb = player.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-150f, 400f));

        // Tilt player object 45 degrees
        player.transform.Rotate(new Vector3(0, 0, 45f));

        // Disable all player controls
        player.GetComponent<PlayerCtrl>().enabled = false;

        // Disable all colliders of the player object
        foreach(Collider2D c2d in player.transform.GetComponents<Collider2D>())
        {
            c2d.enabled = false;
        }

        // Disable child objects of the player object - they may have coliders and other
        foreach(Transform childTransform in player.transform)
        {
            childTransform.gameObject.SetActive(false);
        }

        // Disable the camera movement
        Camera.main.GetComponent<CamerCtrl>().enabled = false;

        // Set the velocity of the player to 0
        rb.velocity = Vector2.zero;

        // Restart level with a coroutine -> we cannot pass parameters (player) with invoke
        StartCoroutine("PauseBeforeReload", player);
    }

    IEnumerator PauseBeforeReload(GameObject player)
    {
        yield return new WaitForSeconds(1.5f);
        PlayerDied(player);
    }

    public void BulletHitEnemy(Transform enemy)
    {
        Vector3 enemyPos = enemy.position;
        enemyPos.z = 20f;
        SFXCtrl.instance.ShowEnemyExplosion(enemyPos);

        Destroy(enemy.gameObject);

        Instantiate(bigCoin, enemyPos, Quaternion.identity);

        Destroy(enemy.gameObject);
    }
}
