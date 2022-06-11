using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;

/// <summary>
/// ���ű����ڿ���ÿһ�����������
/// </summary>
/// 

public class myHash : IEqualityComparer<Vector2>
{
    public bool Equals(Vector2 a, Vector2 b)
    {
        return (a.x == b.x && a.y == b.y);
    }
    public int GetHashCode(Vector2 a)
    {
        int x = (int)a.x / 36, y = (int)a.y / 22, mx = Mathf.Abs(x), my = Mathf.Abs(y);
        return (mx << 12) | (my << 2)
            | ((x == mx ? 1 : 0) << 1) | ((y == my ? 1 : 0));
    }
}


public class RoomGenerator : MonoBehaviour
{
    public enum Direction
    {
        up, down, left, right
    }

    [Header("����������Ϣ")]
    public List<GameObject> roomPrefabs;   //���ɷ����Ԥ�Ƽ���������õķ���ŵ����list�
    public int roomNumber;  //��Ҫһ�����ɶ��ٸ�����
    public Color startColor, endColor;  //��һ����������һ���������ɫ
    private GameObject endroom; //���һ������
    public LayerMask roomLayer;

    [Header("����λ�ÿ���")]
    //�˲�����д�꣬��̫��Ҫ��ע
    public Transform generatorPoint;    //�������ɷ�����Ǹ����ĵ�,�������λ��
    public float xOffset;   //���ĵ���X������һ���ƶ��ľ���
    public float yOffset;   //���ĵ���Y������һ���ƶ��ľ���
    public Direction moveDirection; //���ĵ��ƶ��ĳ���
    private HashSet<Vector2> roomPositionSet = new HashSet<Vector2>(100000, new myHash()); //�Ѿ����ɵķ����λ�õļ���
    private int[] maxPosition = new int[5]; //��߽���ĸ���
    private int[] maxPositionX = new int[5];
    private int[] maxPositionY = new int[5];


    public List<Room> rooms = new List<Room>(); //ȫ�巿�䣬��Ҫ��Unity�ڸ����
    public WallList wallType;   //����ǽ�ڵ����ͣ����ǽ�ڷŵ����


    void Start()
    {
        //����roomNumber������
        GenerateRooms();

        //���ݷ��䲼�����÷����ź�ǽ��
        foreach (var room in rooms)
        {
            SetupRoom(room, room.transform.position);   //��ȡ����������û�з��䣬�������ź�ǽ��
        }

        //��һ����������һ���������ɫ������С��ͼ��
        rooms[0].roomPosition = 1;
        endroom = getEndRoom();
        endroom.GetComponent<Room>().roomPosition = 2;


    }

    //�������ɷ���ĺ���
    /*********�����߼�************
        * 1.��ʼ����boss���̶�ˢ��
        * 2.ǰ������俴��ˢ������һ���̵귿
        * 3.boss��ǰһ������̶�ˢ���̵귿����ν��ս֮ǰ���в�����
        * 4.���෿�����ˢ�����﷿
    ****************************/
    public void GenerateRooms()
    {
        bool isShop = false; //�Ƿ���ֹ��̵�
        //����roomNumber������
        for (int i = 0; i < roomNumber; i++)
        {
            //�����������һ������ʱ�����ĵ�λ������
            if (i == roomNumber - 1)
            {
                getEndGeneratorPointPosition();
            }

            //��ʼ������boss������Ҫ��������
            if (i == 0)
            {
                rooms.Add(Instantiate(roomPrefabs[0], generatorPoint.position, Quaternion.identity).GetComponent<Room>());
            }
            else if (i == roomNumber - 1)
            {
                rooms.Add(Instantiate(roomPrefabs[roomPrefabs.Count - 1], generatorPoint.position, Quaternion.identity).GetComponent<Room>());
            }
            else if (i == roomNumber - 2)
            {
                rooms.Add(Instantiate(roomPrefabs[roomPrefabs.Count - 2], generatorPoint.position, Quaternion.identity).GetComponent<Room>());
            }
            else
            {
                if (i <= 5)
                {
                    if (isShop)
                    {
                        int rd = Random.Range(1, roomPrefabs.Count - 2);  //������ֹ��̵꣬�Ͳ���ˢ��
                        rooms.Add(Instantiate(roomPrefabs[rd], generatorPoint.position, Quaternion.identity).GetComponent<Room>());
                    }
                    else
                    {
                        int rd = Random.Range(1, roomPrefabs.Count - 1);    //֮���Դ�1��count-1������Ϊ��һ���ǳ�ʼ���䣬���һ����boss���䣬��Ҫ��������
                        if (rd == roomPrefabs.Count - 2)
                        {
                            isShop = true;
                        }
                        rooms.Add(Instantiate(roomPrefabs[rd], generatorPoint.position, Quaternion.identity).GetComponent<Room>());
                    }
                }
                if (i > 5)
                {
                    int rd = Random.Range(1, roomPrefabs.Count - 2);
                    rooms.Add(Instantiate(roomPrefabs[rd], generatorPoint.position, Quaternion.identity).GetComponent<Room>());
                }
            }
            //����ı��������ĵ��λ��
            changeGeneratorPointPosition();
        }
    }

    //��ȡ���һ�����䣬Ҳ����BOSS���䣬Ҳ��ͨ����һ������
    public GameObject getEndRoom()
    {
        GameObject ret = rooms[roomNumber - 1].gameObject;

        return ret;
    }

    void Update()
    {
        //���if���ڲ��Է���������� ��ʽ��Ϸ��ǵ�ɾ�� Ч����ÿ����һ�ΰ����ظ�����һ�߱�Scene���������ɷ��䣩
        /*if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }*/
    }

    /// <summary>
    /// �������ݼ�������Ҫ�޸�
    /// ��������ǽ�ڵĺ���SetupRoom
    /// �����ֻҪ����һ�����͵�ǽ�ڣ���ô���random��ȥ��
    /// 
    /// </summary>

    //�����������һ����������꣬������
    public void getEndGeneratorPointPosition()
    {
        int judge = 0;
        do
        {
            judge = 0;
            int rd = Random.Range(1, 5);
            int ra = Random.Range(0, 2);
            generatorPoint.position = new Vector3(
                maxPositionX[rd] + xOffset * ra * (Random.Range(0, 2) == 1 ? 1 : -1),
                maxPositionY[rd] + yOffset * (1 - ra) * (Random.Range(0, 2) == 1 ? 1 : -1),
                0);
            judge += roomPositionSet.Contains(new Vector2(generatorPoint.position.x - xOffset, generatorPoint.position.y)) ? 1 : 0;
            judge += roomPositionSet.Contains(new Vector2(generatorPoint.position.x + xOffset, generatorPoint.position.y)) ? 1 : 0;
            judge += roomPositionSet.Contains(new Vector2(generatorPoint.position.x, generatorPoint.position.y - yOffset)) ? 1 : 0;
            judge += roomPositionSet.Contains(new Vector2(generatorPoint.position.x, generatorPoint.position.y + yOffset)) ? 1 : 0;
            if (Mathf.Abs(generatorPoint.position.x) <= xOffset && Mathf.Abs(generatorPoint.position.y) <= yOffset)
            {
                judge = 0;
            }

        } while (judge != 1 || roomPositionSet.Contains(new Vector2(generatorPoint.position.x, generatorPoint.position.y)));
    }

    //������������µ����ĵ�����, ������, ��֮���ڱ�֤������"����"�����������
    public void changeGeneratorPointPosition()
    {
        moveDirection = (Direction)Random.Range(0, 3 + 1);
        Vector2 generatorPointPositionNow; generatorPointPositionNow.x = generatorPoint.position.x; generatorPointPositionNow.y = generatorPoint.position.y;

        roomPositionSet.Add(generatorPointPositionNow);

        if (generatorPoint.position.x + generatorPoint.position.y > maxPosition[1])
        {
            maxPosition[1] = (int)generatorPoint.position.x + (int)generatorPoint.position.y;
            maxPositionX[1] = (int)generatorPoint.position.x; maxPositionY[1] = (int)generatorPoint.position.y;
        }
        if (-generatorPoint.position.x + generatorPoint.position.y > maxPosition[2])
        {
            maxPosition[2] = -(int)generatorPoint.position.x + (int)generatorPoint.position.y;
            maxPositionX[2] = (int)generatorPoint.position.x; maxPositionY[2] = (int)generatorPoint.position.y;
        }
        if (-generatorPoint.position.x - generatorPoint.position.y > maxPosition[3])
        {
            maxPosition[3] = -(int)generatorPoint.position.x - (int)generatorPoint.position.y;
            maxPositionX[3] = (int)generatorPoint.position.x; maxPositionY[3] = (int)generatorPoint.position.y;
        }
        if (generatorPoint.position.x - generatorPoint.position.y > maxPosition[4])
        {
            maxPosition[4] = (int)generatorPoint.position.x - (int)generatorPoint.position.y;
            maxPositionX[4] = (int)generatorPoint.position.x; maxPositionY[4] = (int)generatorPoint.position.y;
        }

        Vector2 generatorPointPositionNextUp;
        generatorPointPositionNextUp.x = generatorPoint.position.x; generatorPointPositionNextUp.y = generatorPoint.position.y + yOffset;
        Vector2 generatorPointPositionNextDown;
        generatorPointPositionNextDown.x = generatorPoint.position.x; generatorPointPositionNextDown.y = generatorPoint.position.y - yOffset;
        Vector2 generatorPointPositionNextLeft;
        generatorPointPositionNextLeft.x = generatorPoint.position.x - xOffset; generatorPointPositionNextLeft.y = generatorPoint.position.y;
        Vector2 generatorPointPositionNextRight;
        generatorPointPositionNextRight.x = generatorPoint.position.x + xOffset; generatorPointPositionNextRight.y = generatorPoint.position.y;

        bool[] nextCanGenerate = new bool[4];
        int canGenerateNumber = 0;
        nextCanGenerate[0] = !roomPositionSet.Contains(generatorPointPositionNextUp);
        nextCanGenerate[1] = !roomPositionSet.Contains(generatorPointPositionNextDown);
        nextCanGenerate[2] = !roomPositionSet.Contains(generatorPointPositionNextLeft);
        nextCanGenerate[3] = !roomPositionSet.Contains(generatorPointPositionNextRight);

        for (int i = 0; i < 4; i++)
        {
            canGenerateNumber += nextCanGenerate[i] ? 1 : 0;
        }
        if (canGenerateNumber == 0)
        {
            do
            {
                int rd = Random.Range(1, 5);
                int ra = Random.Range(0, 2);
                generatorPoint.position = new Vector3(
                    maxPositionX[rd] + xOffset * ra * (Random.Range(0, 2) == 1 ? 1 : -1),
                    maxPositionY[rd] + yOffset * (1 - ra) * (Random.Range(0, 2) == 1 ? 1 : -1),
                    0);
            } while (!roomPositionSet.Contains(new Vector2(generatorPoint.position.x, generatorPoint.position.y)));
            return;
        }
        while (!nextCanGenerate[(int)moveDirection])
        {
            moveDirection = (Direction)Random.Range(0, 4);
        }

        if (moveDirection == Direction.up)
        {
            generatorPoint.position += new Vector3(0, yOffset, 0);
        }
        if (moveDirection == Direction.down)
        {
            generatorPoint.position += new Vector3(0, -yOffset, 0);
        }
        if (moveDirection == Direction.left)
        {
            generatorPoint.position += new Vector3(-xOffset, 0, 0);
        }
        if (moveDirection == Direction.right)
        {
            generatorPoint.position += new Vector3(xOffset, 0, 0);
        }
    }


    //�жϷ��䴴���������(ǽ��)
    public void SetupRoom(Room newRoom, Vector3 roomPosition)
    {
        //�ж����������Ƿ��з���
        newRoom.roomLeft = roomPositionSet.Contains(new Vector2(roomPosition.x - xOffset, roomPosition.y));
        newRoom.roomRight = roomPositionSet.Contains(new Vector2(roomPosition.x + xOffset, roomPosition.y));
        newRoom.roomUp = roomPositionSet.Contains(new Vector2(roomPosition.x, roomPosition.y + yOffset));
        newRoom.roomDown = roomPositionSet.Contains(new Vector2(roomPosition.x, roomPosition.y - yOffset));

        //5.9��������,�ж����һ������(BOSS)
        newRoom.bossLeft = (new Vector3(roomPosition.x - xOffset, roomPosition.y, 0)
            == rooms[roomNumber - 1].gameObject.transform.position);
        newRoom.bossRight = (new Vector3(roomPosition.x + xOffset, roomPosition.y, 0)
            == rooms[roomNumber - 1].gameObject.transform.position);
        newRoom.bossUp = (new Vector3(roomPosition.x, roomPosition.y + yOffset, 0)
            == rooms[roomNumber - 1].gameObject.transform.position);
        newRoom.bossDown = (new Vector3(roomPosition.x, roomPosition.y - yOffset, 0)
            == rooms[roomNumber - 1].gameObject.transform.position);

        newRoom.UpdateRoom(xOffset, yOffset);
        int randomRoomType = Random.Range(0, wallType.wallList.Count);  //�˴������ѡ��ǽ������
                                                                        //����㲻��Ҫ����͸ĵ��������

        //���ݷ���ѡ�񷿼�

        if (newRoom.doorNumber == 1)
        {
            Instantiate(wallType.wallList[randomRoomType].singleUp, roomPosition, Quaternion.identity);
        }
        if (newRoom.doorNumber == 2)
        {
            Instantiate(wallType.wallList[randomRoomType].singleDown, roomPosition, Quaternion.identity);
        }
        if (newRoom.doorNumber == 3)
        {
            Instantiate(wallType.wallList[randomRoomType].doubleUD, roomPosition, Quaternion.identity);
        }
        if (newRoom.doorNumber == 4)
        {
            Instantiate(wallType.wallList[randomRoomType].singleLeft, roomPosition, Quaternion.identity);
        }
        if (newRoom.doorNumber == 5)
        {
            Instantiate(wallType.wallList[randomRoomType].doubleUL, roomPosition, Quaternion.identity);
        }
        if (newRoom.doorNumber == 6)
        {
            Instantiate(wallType.wallList[randomRoomType].doubleDL, roomPosition, Quaternion.identity);
        }
        if (newRoom.doorNumber == 7)
        {
            Instantiate(wallType.wallList[randomRoomType].tripleUDL, roomPosition, Quaternion.identity);
        }
        if (newRoom.doorNumber == 8)
        {
            Instantiate(wallType.wallList[randomRoomType].singleRight, roomPosition, Quaternion.identity);
        }
        if (newRoom.doorNumber == 9)
        {
            Instantiate(wallType.wallList[randomRoomType].doubleUR, roomPosition, Quaternion.identity);
        }
        if (newRoom.doorNumber == 10)
        {
            Instantiate(wallType.wallList[randomRoomType].doubleDR, roomPosition, Quaternion.identity);
        }
        if (newRoom.doorNumber == 11)
        {
            Instantiate(wallType.wallList[randomRoomType].tripleUDR, roomPosition, Quaternion.identity);
        }
        if (newRoom.doorNumber == 12)
        {
            Instantiate(wallType.wallList[randomRoomType].doubleLR, roomPosition, Quaternion.identity);
        }
        if (newRoom.doorNumber == 13)
        {
            Instantiate(wallType.wallList[randomRoomType].tripleULR, roomPosition, Quaternion.identity);
        }
        if (newRoom.doorNumber == 14)
        {
            Instantiate(wallType.wallList[randomRoomType].tripleDLR, roomPosition, Quaternion.identity);
        }
        if (newRoom.doorNumber == 15)
        {
            Instantiate(wallType.wallList[randomRoomType].fourUDLR, roomPosition, Quaternion.identity);
        }
    }
}


