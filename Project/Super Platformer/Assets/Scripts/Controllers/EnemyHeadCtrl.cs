using UnityEngine;

public class EnemyHeadCtrl : MonoBehaviour
{
    public GameObject enemy;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PlayerFeet"))
        {
            GameCtrl.instance.PlayerStompsEnemy(enemy);

            SFXCtrl.instance.ShowEnemyPoof(enemy.transform.position);

            AudioCtrl.instance.EnemyHit(enemy.transform.position);
        }
    }
}
