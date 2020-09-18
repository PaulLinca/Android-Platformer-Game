using UnityEngine;
using DG.Tweening;

/// <summary>
/// The AI Engine of the Bomber Bee
/// </summary>
public class BomberBeeAI : MonoBehaviour
{
    public float destoryBeeDelay;
    public float beeSpeed;
    
    public void ActivateBee(Vector3 playerPos)
    {
        transform.DOMove(playerPos, beeSpeed, false);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player"))
        {
            SFXCtrl.instance.ShowEnemyExplosion(other.gameObject.transform.position);
            Destroy(gameObject, destoryBeeDelay);
        }
    }
}
