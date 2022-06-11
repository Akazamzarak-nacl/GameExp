using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public int addHealthAmount;//设置每个血包的增值，后面同学可以直接通过预制体修改
    public AudioClip healthCollectClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();
        if(playerController!=null)
        {
            //判断是否满血
            if (playerController.getCurrentHealth() < playerController.maxHealth)
            {
                playerController.ChangeHealth(addHealthAmount);//改变血量
                AudioManager.instance.AudioPlay(healthCollectClip);//播放音效
                Destroy(gameObject);
            }
        }
        
    }
}
