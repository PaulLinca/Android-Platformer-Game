using UnityEngine;

/// <summary>
/// Controller for the sign that triggers the boss battle
/// </summary>
public class SignCtrl : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameCtrl.instance.StopCameraFollow();       // Fix the camer
            GameCtrl.instance.ActivateEnemySpawner();   // Activate the monster spawner
        }
    }
}
