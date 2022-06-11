using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制相机移动
/// </summary>

public class CameraController : MonoBehaviour
{

    public static CameraController instance;


    [Header("相机参数")]
    public float speed; //相机移动速度
    public Transform target;    //目标

    private void Awake()
    {
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        //移动到下一个房间
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(target.position.x, target.position.y, transform.position.z),
                speed * Time.deltaTime);
        }
    }

    //下一个房间坐标
    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }

}
