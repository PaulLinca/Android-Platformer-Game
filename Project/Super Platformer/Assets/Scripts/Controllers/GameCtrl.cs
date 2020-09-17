using System.Runtime.Serialization.Formatters.Binary;
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
    bool timerOn;

    public int coinScoreValue;
    public int bigCoinScoreValue;
    public int enemyScoreValue;

    public float maxTime;
    public UI ui;
    public GameObject bigCoin;
    public GameObject player;
    public GameObject lever;
    public GameObject enemySpawner;
    public GameObject signPlatform;
    
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
        DataCtrl.instance.RefreshData();
        data = DataCtrl.instance.data;
        RefreshUI();
        
        timeLeft = maxTime;

        timerOn = true;

        HandleFirstBoot();
        UpdateHearts();

        ui.bossHealth.gameObject.SetActive(false);
    }

    void Update()
    {
        if(timeLeft > 0 && timerOn)
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

    public void RefreshUI()
    {
        Debug.Log("Number of coins = " + data.coinCount);
        ui.textCoinCount.text = $" x {data.coinCount}";
        ui.textScore.text = $"Score: {data.score}";
    }
   
    void OnEnable()
    {
        Debug.Log("Data Loaded");
        RefreshUI();
    }   

    void OnDisable()
    {
        Debug.Log("Data Saved");
        DataCtrl.instance.SaveData(data);
    }

    public int GetScore()
    {
        return data.score;
    }

    public void SetStarsAwarded(int levelNumber, int numOfStars)
    {
        data.levelData[levelNumber].starsAwarded = numOfStars;
        Debug.Log("Set stars awarder");
    }

    public void UnlockLevel(int levelNumber)
    {
        data.levelData[levelNumber].isUnlocked = true;
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
            DataCtrl.instance.SaveData(data);
            
            Invoke("GameOver", restartDelay);
        }
        else
        {
            DataCtrl.instance.SaveData(data);
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

        if(data.keyFound[0] && data.keyFound[1])
        {
            ShowSignPlatform();
        }
    }

    void ShowSignPlatform()
    {
        signPlatform.SetActive(true);
        SFXCtrl.instance.ShowPlayerLanding(signPlatform.transform.position);

        timerOn = false;
        
        ui.bossHealth.gameObject.SetActive(false);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

        AudioCtrl.instance.EnemyExplosion(enemyPos);
    }

    public void PlayerStompsEnemy(GameObject enemy)
    {
        enemy.tag = "Untagged";

        Destroy(enemy);

        UpdateScore(Item.Enemy);
    }

    public void StopCameraFollow()
    {
        Camera.main.GetComponent<CamerCtrl>().enabled = false;

        //disable parallax
        player.GetComponent<PlayerCtrl>().isStuck = true;
        player.transform.Find("LeftCheck").gameObject.SetActive(false);
        player.transform.Find("RightCheck").gameObject.SetActive(false);
    }

    public void ShowLever()
    {
        lever.SetActive(true);
        SFXCtrl.instance.ShowPlayerLanding(lever.gameObject.transform.position);
        AudioCtrl.instance.EnemyExplosion(lever.gameObject.transform.position);

        DectivateEnemySpawner();
    }

    public void ActivateEnemySpawner()
    {
        enemySpawner.SetActive(true);
    }

    public void DectivateEnemySpawner()
    {
        enemySpawner.SetActive(false);
    }

    public void LevelComplete()
    {
        ui.panelMobileUI.SetActive(false);
        ui.levelCompleteMenu.SetActive(true);
    }
}
