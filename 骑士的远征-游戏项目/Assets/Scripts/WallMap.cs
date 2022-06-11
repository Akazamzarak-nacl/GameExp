using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 请将此脚本挂载到墙体预制体的WallMap上
/// 此脚本用于小地图制作
/// </summary>

public class WallMap : MonoBehaviour
{
    public GameObject mapSprite;

    private void OnEnable()
    {
        mapSprite = transform.parent.GetChild(0).gameObject;//获取Map_Rooms 1_1

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
