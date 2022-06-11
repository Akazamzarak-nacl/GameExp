using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 请将此脚本挂载到房间预制体上
/// </summary>

public class Room : MonoBehaviour
{
    [Header("房门信息")]
    public GameObject doorLeft, doorRight, doorUp, doorDown;    //房门
    public bool roomLeft, roomRight, roomUp, roomDown;  //是否有房间
    public bool bossLeft, bossRight, bossUp, bossDown;  //5.9新增 四周是否有boss
    public int doorNumber;  //房门数量
    public List<GameObject> destructibleThings;    //箱子，桶，罐子
    public bool playerin;

    [Header("用于小地图")]
    public int roomPosition;    // 0中间房 1开始 2最终房
    public List<GameObject> roomForLittleMap;
    public Bounds mybounds;
    public Transform player;

    [Header("房间信息")]
    public int stepToStart; //到开始点的曼哈顿距离

    // Start is called before the first frame update
    void Start()
    {
        //用于小地图标记走过的路
        foreach (GameObject g in roomForLittleMap)
        {
            g.SetActive(false);
        }
        roomForLittleMap[roomPosition].SetActive(true);
        //5.9更新 根据四周设置房门的显示值
        doorLeft.SetActive(roomLeft && !bossLeft);
        doorUp.SetActive(roomUp && !bossUp);
        doorDown.SetActive(roomDown && !bossDown);
        doorRight.SetActive(roomRight && !bossRight);

        //用于随机生成可破坏物品
        for (int i = 0; i < destructibleThings.Count; i++)
        {
            destructibleThings[i].SetActive(Random.Range(0, 2) == 1 ? true : false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        mybounds = this.transform.Find("RoomArea").gameObject.GetComponent<BoxCollider2D>().bounds;
        player = GameObject.Find("Player").transform;
        Vector3 pos = player.position;
        if (mybounds.Contains(pos))
        {
            playerin = true;
            EnemyStats[] childrenList = GetComponentsInChildren<EnemyStats>();
            foreach (EnemyStats child in childrenList)
            {
                child.setin(true);
            }
            BossStats[] childrenList1 = GetComponentsInChildren<BossStats>();
            foreach (BossStats child in childrenList1)
            {
                child.setin(true);
            }
        }
        else
        {
            playerin = false;
            EnemyStats[] childrenList = GetComponentsInChildren<EnemyStats>();
            foreach (EnemyStats child in childrenList)
            {
                child.setin(false);
            }
            BossStats[] childrenList1 = GetComponentsInChildren<BossStats>();
            foreach (BossStats child in childrenList1)
            {
                child.setin(true);
            }
        }
    }

    //更新房间信息
    public void UpdateRoom(float xOffset, float yOffset)
    {
        stepToStart = (int)Mathf.Abs(transform.position.x / xOffset) + (int)Mathf.Abs(transform.position.y / yOffset);
        //此处使用了压位，看不懂可以问我，当然你应该不需要看懂这个
        if (roomUp)
        {
            doorNumber |= (1 << 0);
        }
        if (roomDown)
        {
            doorNumber |= (1 << 1);
        }
        if (roomLeft)
        {
            doorNumber |= (1 << 2);
        }
        if (roomRight)
        {
            doorNumber |= (1 << 3);
        }

    }

    //检测人物是否碰撞 用于相机转换房间
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraController.instance.ChangeTarget(transform);  //变换相机坐标
            
        }
    }

}


