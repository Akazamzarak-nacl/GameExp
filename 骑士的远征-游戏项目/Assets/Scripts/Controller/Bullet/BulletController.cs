using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public bool isActive=false;
    public Rigidbody2D rb;
    public WeaponData_SO weaponData;
    public GameObject explosionPrefab;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {

    }

    
    //伤害计算函数！！！！
    private void OnTriggerEnter2D(Collider2D collision)   //注意相应碰撞体改成trigger
    {
        if(CompareTag("PlayerBullet")&&collision.CompareTag("Enemy"))   //直接用tag==这种方法inefficient
        {
            collision.gameObject.GetComponent<EnemyStats>().TakeDamage(weaponData);
            gameObject.SetActive(false);
            isActive = false;
        }
        else if(CompareTag("PlayerBullet") && collision.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<BossStats>().TakeDamage(weaponData);
            gameObject.SetActive(false);
            isActive = false;
        }
        else if(collision.CompareTag("Wall"))
        {
            try
            {
                GameObject exp = ObjectPool.Instance.GetObject(explosionPrefab);
                exp.transform.position = transform.position;
            }
            catch(Exception e)
            {
                ;
            }
            gameObject.SetActive(false);
            isActive = false;
        }
        else if(CompareTag("EnemyBullet")&&collision.CompareTag("Player"))
        {
            //int damage = Random.Range(weaponData.minDamage, weaponData.maxDamage + 1);
            collision.gameObject.GetComponent<PlayerController>().ChangeHealth(-2);
            gameObject.SetActive(false);
            isActive = false;
        }
    }
}
