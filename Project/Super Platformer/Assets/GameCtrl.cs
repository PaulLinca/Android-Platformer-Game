using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCtrl : MonoBehaviour
{
    public static GameCtrl instance;
    public float restartDelay;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
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
