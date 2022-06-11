using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }
    private void Start()
    {
        instance = this;
    }
    public Image healthBar;//血条
    public Text coinConutText;//金币


    public void UpdateHealthBar(int curAmount ,int maxAmount)//更新血条
    {
        healthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }

    public void UpdateCoinCount(int curAmount)//更新金币数量
    {
        coinConutText.text = curAmount.ToString();
    }
}
