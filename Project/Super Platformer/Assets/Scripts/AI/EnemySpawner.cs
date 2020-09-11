using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnDelay;

    bool canSpawn;

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawn)
        {
            StartCoroutine("SpawnEnemies");
        }
    }

    IEnumerator SpawnEnemies()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
        canSpawn = false;
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }
}
