using System.Collections;
using UnityEngine;


/// <summary>
/// Spawns enemies during the boss battle
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnDelay;

    bool canSpawn;

    void Start()
    {
        canSpawn = true;
    }

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
