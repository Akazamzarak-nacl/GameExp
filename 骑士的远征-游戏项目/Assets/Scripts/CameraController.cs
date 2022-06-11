using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������ƶ�
/// </summary>

public class CameraController : MonoBehaviour
{

    public static CameraController instance;


    [Header("�������")]
    public float speed; //����ƶ��ٶ�
    public Transform target;    //Ŀ��

    private void Awake()
    {
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        //�ƶ�����һ������
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(target.position.x, target.position.y, transform.position.z),
                speed * Time.deltaTime);
        }
    }

    //��һ����������
    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }

}
