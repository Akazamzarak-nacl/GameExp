using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHealth : MonoBehaviour
{
    public void showHealth()
    {
        Text text = GameObject.Find("HealthText").GetComponent<Text>();
        Slider slider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        text.text = "Health: " + slider.value;
    }
}
