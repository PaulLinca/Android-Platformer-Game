using System.Collections;
using UnityEngine;

/// <summary>
/// This script spawns or shows the jumping fish, every spawnDelay number of seconds
/// </summary>
public class FishSpawnerScript : MonoBehaviour
{
    public GameObject fish;
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
            StartCoroutine("SpawnFish");
        }
    }

    IEnumerator SpawnFish()
    {
        Instantiate(fish, transform.position, Quaternion.identity);
        canSpawn = false;
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }
}
