using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public GameObject LevelSettingMenu;
    public GameObject MainMenu;
    public GameObject HelpMenu;//�����˵�
    public GameObject AboutMenu;//���ڲ˵�


    public float level = 1;//�Ѷ�����0�� 1��ͨ 2����

    public void OnPlay()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;

    }
    public void quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void About()
    {
        MainMenu.SetActive(false);
        AboutMenu.SetActive(true);
    }
    public void OnHelp()
    {
        MainMenu.SetActive(false);
        HelpMenu.SetActive(true);
    }
    public void OnLevelSetting()
    {
        MainMenu.SetActive(false);
        LevelSettingMenu.SetActive(true);
    }
    public void OnClose()//�ر��Ѷ�ѡ��˵�
    {
        MainMenu.SetActive(true);
        LevelSettingMenu.SetActive(false);
    }

    public void OnClose2()//�رհ����˵�
    {
        HelpMenu.SetActive(false);
        MainMenu.SetActive(true);

    }
    public void OnClose3()//�رչ��ڲ˵�
    {
        AboutMenu.SetActive(false);
        MainMenu.SetActive(true);

    }

    public void OnSimple()//ѡ���
    {
        level = 0;
        Debug.Log(level);
    }

    public void OnNormal()//ѡ����ͨ
    {
        level = 1;
        Debug.Log(level);
    }
    public void OnHard()//ѡ������
    {
        level = 2;
        Debug.Log(level);
    }
}
