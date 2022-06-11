using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarCollectible : MonoBehaviour
{
    public GameObject coin;
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;
    public GameObject health4;
    public AudioClip JarBrokenClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int random = Random.Range(1, 9);
       // Debug.Log("Random" + random);
       // PlayerController playerController = collision.GetComponent<PlayerController>();
        //WeaponController weaponController = collision.GetComponent<WeaponController>();//获取武器的碰撞器
        //if (weaponController != null)
        // {
                if (collision.gameObject.tag=="PlayerBullet")
                {
                    AudioManager.instance.AudioPlay(JarBrokenClip);
                    Destroy(gameObject);
                    if(random>=1&&random<=4)
                    { GameObject instance = (GameObject)Instantiate(coin, transform.position, transform.rotation);//在罐子的位置显示金币
                     
                    }
                    else if(random==5)
                    {
                    GameObject instance = (GameObject)Instantiate(health1,transform.position, transform.rotation);
                    }
                    else if (random == 6)
                    {
                        GameObject instance = (GameObject)Instantiate(health2, transform.position, transform.rotation);
                    }
                    else if (random == 7)
                    {
                        GameObject instance = (GameObject)Instantiate(health3, transform.position, transform.rotation);
                    }
                    else if (random == 8)
                    {
                        GameObject instance = (GameObject)Instantiate(health4, transform.position, transform.rotation);
                    }
        }

            
        //}

    }
}
