using UnityEngine;

public class CoinCtrl : MonoBehaviour
{
    public enum CoinFX
    {
        Vanish,
        Fly
    }

    public CoinFX coinFX;
    public float flySpeed;
    public bool startFlying;

    GameObject coinMeter;

    void Start()
    {
        startFlying = false;

        if(coinFX == CoinFX.Fly)
        {
            coinMeter = GameObject.Find("img_coin_count");
        }
    }

    void Update()
    {
        if(startFlying)
        {
            transform.position = Vector3.Lerp(transform.position, coinMeter.transform.position, flySpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(coinFX == CoinFX.Vanish)
            {
                Destroy(gameObject);
            }
            else if(coinFX == CoinFX.Fly)
            {
                gameObject.layer = 0;
                startFlying = true;
            }
        }
    }
}
