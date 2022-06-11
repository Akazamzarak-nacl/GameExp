using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuButton : MonoBehaviour
{
    public GameObject pauseMenu;//获取暂定菜单
    public GameObject settingMenu;//获取设置菜单
    
    public void OnPause()//暂停
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    public void OnPlay()//开始
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    public void OnReturn()//返回主菜单
    {
        PlayerController.weaponNum.RemoveRange(0, PlayerController.weaponNum.Count);
        PlayerController.gunNum = 0;
        PlayerController.instance.setStartCurrentHealth();//设置初始血量
        PlayerController.instance.setStartCoinCount();//设置初始金币数
        shopController.rocket = false;
        shopController.shotgun = false;
        SceneManager.LoadScene(0);
        
    }

    public void OnSet()//打开设置菜单
    {
        pauseMenu.SetActive(false);
        settingMenu.SetActive(true);
    }

    public void OnClose()//关闭setting菜单
    {
        settingMenu.SetActive(false);
        pauseMenu.SetActive(true);

    }

}
