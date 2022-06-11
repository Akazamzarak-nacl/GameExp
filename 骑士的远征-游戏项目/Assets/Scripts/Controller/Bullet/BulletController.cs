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

    
    //�˺����㺯����������
    private void OnTriggerEnter2D(Collider2D collision)   //ע����Ӧ��ײ��ĳ�trigger
    {
        if(CompareTag("PlayerBullet")&&collision.CompareTag("Enemy"))   //ֱ����tag==���ַ���inefficient
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
