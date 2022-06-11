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
    public Image healthBar;//Ѫ��
    public Text coinConutText;//���


    public void UpdateHealthBar(int curAmount ,int maxAmount)//����Ѫ��
    {
        healthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }

    public void UpdateCoinCount(int curAmount)//���½������
    {
        coinConutText.text = curAmount.ToString();
    }
}
