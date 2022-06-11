using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    private int random;
    public AudioClip CoinCollectClip;
    private void Start()
    {
        random= Random.Range(1, 5);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();
        if (playerController != null)
        {
                playerController.ChangeCoinCount(random);
                AudioManager.instance.AudioPlay(CoinCollectClip);//≤•∑≈“Ù–ß
                Destroy(gameObject);

        }

    }
}
