using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuButton : MonoBehaviour
{
    public GameObject pauseMenu;//��ȡ�ݶ��˵�
    public GameObject settingMenu;//��ȡ���ò˵�
    
    public void OnPause()//��ͣ
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    public void OnPlay()//��ʼ
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    public void OnReturn()//�������˵�
    {
        PlayerController.weaponNum.RemoveRange(0, PlayerController.weaponNum.Count);
        PlayerController.gunNum = 0;
        PlayerController.instance.setStartCurrentHealth();//���ó�ʼѪ��
        PlayerController.instance.setStartCoinCount();//���ó�ʼ�����
        shopController.rocket = false;
        shopController.shotgun = false;
        SceneManager.LoadScene(0);
        
    }

    public void OnSet()//�����ò˵�
    {
        pauseMenu.SetActive(false);
        settingMenu.SetActive(true);
    }

    public void OnClose()//�ر�setting�˵�
    {
        settingMenu.SetActive(false);
        pauseMenu.SetActive(true);

    }

}
