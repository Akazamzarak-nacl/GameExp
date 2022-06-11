using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public Text loadText;
    public Slider loadSlider;
    public int loadTime = 0;
    void Update()
    {
        if (loadTime < 1000)
        {
            loadTime++;
            loadSlider.value = loadTime;
            loadText.text = (loadTime / 10).ToString() + "%";
        }
        else
        {
            SceneManager.LoadScene("LoginSence");
        }
    }
}
