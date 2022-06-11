using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickLogin : MonoBehaviour
{
    public InputField InputField1;
    public InputField InputField2;
    public Text Hint;

    public void OnClick()
    {
        //账号密码不合法
        if (InputField1.text == "" || InputField2.text == "")
        {
            Hint.text = "请输入账号及密码";
        }
        else
        {
            Debug.Log("Welcome: " + InputField1.text); //在Console输出当前账号
            SceneManager.LoadScene("SampleScene");
        }
    }
}
