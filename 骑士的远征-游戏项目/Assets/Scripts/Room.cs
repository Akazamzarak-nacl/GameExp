using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �뽫�˽ű����ص�����Ԥ������
/// </summary>

public class Room : MonoBehaviour
{
    [Header("������Ϣ")]
    public GameObject doorLeft, doorRight, doorUp, doorDown;    //����
    public bool roomLeft, roomRight, roomUp, roomDown;  //�Ƿ��з���
    public bool bossLeft, bossRight, bossUp, bossDown;  //5.9���� �����Ƿ���boss
    public int doorNumber;  //��������
    public List<GameObject> destructibleThings;    //���ӣ�Ͱ������
    public bool playerin;

    [Header("����С��ͼ")]
    public int roomPosition;    // 0�м䷿ 1��ʼ 2���շ�
    public List<GameObject> roomForLittleMap;
    public Bounds mybounds;
    public Transform player;

    [Header("������Ϣ")]
    public int stepToStart; //����ʼ��������پ���

    // Start is called before the first frame update
    void Start()
    {
        //����С��ͼ����߹���·
        foreach (GameObject g in roomForLittleMap)
        {
            g.SetActive(false);
        }
        roomForLittleMap[roomPosition].SetActive(true);
        //5.9���� �����������÷��ŵ���ʾֵ
        doorLeft.SetActive(roomLeft && !bossLeft);
        doorUp.SetActive(roomUp && !bossUp);
        doorDown.SetActive(roomDown && !bossDown);
        doorRight.SetActive(roomRight && !bossRight);

        //����������ɿ��ƻ���Ʒ
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

    //���·�����Ϣ
    public void UpdateRoom(float xOffset, float yOffset)
    {
        stepToStart = (int)Mathf.Abs(transform.position.x / xOffset) + (int)Mathf.Abs(transform.position.y / yOffset);
        //�˴�ʹ����ѹλ���������������ң���Ȼ��Ӧ�ò���Ҫ�������
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

    //��������Ƿ���ײ �������ת������
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraController.instance.ChangeTarget(transform);  //�任�������
            
        }
    }

}


