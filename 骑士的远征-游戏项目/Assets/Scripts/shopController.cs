using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopController : MonoBehaviour
{
    public static bool rocket = false;
    public static bool shotgun = false;
    public GameObject disable1;
    public GameObject disable2;
    public static shopController instance { get; private set; }
    // Start is called before the first frame update
    //public GameObject rocket;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(rocket==true)
        {
            disable1.SetActive(true);
        }
        else
        {
            disable1.SetActive(false);
        }
        if (shotgun == true)
        {
            disable2.SetActive(true);
        }
        else
        {
            disable2.SetActive(false);
        }
    }
    
    public void OnReturn()
    {
        GameObject.Destroy(gameObject);
        Time.timeScale = 1;
       
    }
    public void OnRocket()
    {
        if (PlayerController.instance.getCurrentCoin() >= 4 && rocket==false)
        {
            PlayerController.weaponNum.Add(2);
            rocket = true;
            PlayerController.instance.guns.Add(PlayerController.instance.weapons[2]);
            PlayerController.instance.ChangeCoinCount(-4);
        }
        if(rocket ==true)
        {
            disable1.SetActive(true);
        }
        
    }
    public void OnPistol()
    {
        if (PlayerController.instance.getCurrentCoin() >= 666)
        {
            PlayerController.instance.guns.Add(PlayerController.instance.weapons[0]);
            PlayerController.instance.ChangeCoinCount(-12);
        }
    }
    public void OnShotgun()
    {
        if (PlayerController.instance.getCurrentCoin() >= 2&&shotgun==false)
        {
            PlayerController.weaponNum.Add(1);
            shotgun = true;
            PlayerController.instance.guns.Add(PlayerController.instance.weapons[1]);
            PlayerController.instance.ChangeCoinCount(-2);
        }
        if (shotgun == true)
        {
            disable2.SetActive(true);
        }
    }
    public void OnRifle()
    {
        if (PlayerController.instance.getCurrentCoin() >= 9999)
        {
            PlayerController.weaponNum.Add(3);
            PlayerController.instance.guns.Add(PlayerController.instance.weapons[3]);
            PlayerController.instance.ChangeCoinCount(-9999);
        }
    }
    public void OnLasergun()
    {
        if (PlayerController.instance.getCurrentCoin() >= 9999)
        {
            PlayerController.weaponNum.Add(4);
            PlayerController.instance.guns.Add(PlayerController.instance.weapons[4]);
            PlayerController.instance.ChangeCoinCount(-9999);
        }
    }
  
}
