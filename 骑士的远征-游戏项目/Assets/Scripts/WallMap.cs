using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �뽫�˽ű����ص�ǽ��Ԥ�����WallMap��
/// �˽ű�����С��ͼ����
/// </summary>

public class WallMap : MonoBehaviour
{
    public GameObject mapSprite;

    private void OnEnable()
    {
        mapSprite = transform.parent.GetChild(0).gameObject;//��ȡMap_Rooms 1_1

        mapSprite.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mapSprite.SetActive(true);
        }
    }

}
