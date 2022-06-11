using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public int addHealthAmount;//����ÿ��Ѫ������ֵ������ͬѧ����ֱ��ͨ��Ԥ�����޸�
    public AudioClip healthCollectClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();
        if(playerController!=null)
        {
            //�ж��Ƿ���Ѫ
            if (playerController.getCurrentHealth() < playerController.maxHealth)
            {
                playerController.ChangeHealth(addHealthAmount);//�ı�Ѫ��
                AudioManager.instance.AudioPlay(healthCollectClip);//������Ч
                Destroy(gameObject);
            }
        }
        
    }
}
