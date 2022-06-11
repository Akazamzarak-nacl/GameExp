using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;

/// <summary>
/// 本脚本用于控制每一个房间的生成
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

    [Header("房间自身信息")]
    public List<GameObject> roomPrefabs;   //生成房间的预制件，请把做好的房间放到这个list里！
    public int roomNumber;  //你要一共生成多少个房间
    public Color startColor, endColor;  //第一个房间和最后一个房间的颜色
    private GameObject endroom; //最后一个房间
    public LayerMask roomLayer;

    [Header("房间位置控制")]
    //此部分已写完，不太需要关注
    public Transform generatorPoint;    //用于生成房间的那个中心点,代表房间的位置
    public float xOffset;   //中心点沿X方向上一次移动的距离
    public float yOffset;   //中心点沿Y方向上一次移动的距离
    public Direction moveDirection; //中心点移动的朝向
    private HashSet<Vector2> roomPositionSet = new HashSet<Vector2>(100000, new myHash()); //已经生成的房间的位置的集合
    private int[] maxPosition = new int[5]; //最边界的四个点
    private int[] maxPositionX = new int[5];
    private int[] maxPositionY = new int[5];


    public List<Room> rooms = new List<Room>(); //全体房间，不要在Unity内改这个
    public WallList wallType;   //房间墙壁的类型，请把墙壁放到这里！


    void Start()
    {
        //生成roomNumber个房间
        GenerateRooms();

        //根据房间布局设置房间门和墙壁
        foreach (var room in rooms)
        {
            SetupRoom(room, room.transform.position);   //获取房间四周有没有房间，来生成门和墙壁
        }

        //第一个房间和最后一个房间的颜色（用于小地图）
        rooms[0].roomPosition = 1;
        endroom = getEndRoom();
        endroom.GetComponent<Room>().roomPosition = 2;


    }

    //用于生成房间的函数
    /*********生成逻辑************
        * 1.初始房与boss房固定刷出
        * 2.前五个房间看脸刷出至多一个商店房
        * 3.boss房前一个房间固定刷出商店房（所谓大战之前必有补给）
        * 4.其余房间随机刷出怪物房
    ****************************/
    public void GenerateRooms()
    {
        bool isShop = false; //是否出现过商店
        //生成roomNumber个房间
        for (int i = 0; i < roomNumber; i++)
        {
            //用于生成最后一个房间时的中心点位置生成
            if (i == roomNumber - 1)
            {
                getEndGeneratorPointPosition();
            }

            //初始房间与boss房间需要单独加载
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
                        int rd = Random.Range(1, roomPrefabs.Count - 2);  //如果出现过商店，就不再刷新
                        rooms.Add(Instantiate(roomPrefabs[rd], generatorPoint.position, Quaternion.identity).GetComponent<Room>());
                    }
                    else
                    {
                        int rd = Random.Range(1, roomPrefabs.Count - 1);    //之所以从1到count-1，是因为第一个是初始房间，最后一个是boss房间，需要单独加载
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
            //随机改变生成中心点的位置
            changeGeneratorPointPosition();
        }
    }

    //获取最后一个房间，也就是BOSS房间，也是通往下一层的入口
    public GameObject getEndRoom()
    {
        GameObject ret = rooms[roomNumber - 1].gameObject;

        return ret;
    }

    void Update()
    {
        //这个if用于测试房间生成情况 正式游戏请记得删除 效果是每按下一次按键重复加载一边本Scene（重新生成房间）
        /*if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }*/
    }

    /// <summary>
    /// 以下内容几乎不需要修改
    /// 除了生成墙壁的函数SetupRoom
    /// 如果你只要生成一种类型的墙壁，那么请把random给去掉
    /// 
    /// </summary>

    //用于生成最后一个房间的坐标，不解释
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

    //用于随机生成新的中心点坐标, 不解释, 总之是在保证房间能"连着"的情况下生成
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


    //判断房间创建房间和门(墙壁)
    public void SetupRoom(Room newRoom, Vector3 roomPosition)
    {
        //判断上下左右是否有房间
        newRoom.roomLeft = roomPositionSet.Contains(new Vector2(roomPosition.x - xOffset, roomPosition.y));
        newRoom.roomRight = roomPositionSet.Contains(new Vector2(roomPosition.x + xOffset, roomPosition.y));
        newRoom.roomUp = roomPositionSet.Contains(new Vector2(roomPosition.x, roomPosition.y + yOffset));
        newRoom.roomDown = roomPositionSet.Contains(new Vector2(roomPosition.x, roomPosition.y - yOffset));

        //5.9新增部分,判断最后一个房间(BOSS)
        newRoom.bossLeft = (new Vector3(roomPosition.x - xOffset, roomPosition.y, 0)
            == rooms[roomNumber - 1].gameObject.transform.position);
        newRoom.bossRight = (new Vector3(roomPosition.x + xOffset, roomPosition.y, 0)
            == rooms[roomNumber - 1].gameObject.transform.position);
        newRoom.bossUp = (new Vector3(roomPosition.x, roomPosition.y + yOffset, 0)
            == rooms[roomNumber - 1].gameObject.transform.position);
        newRoom.bossDown = (new Vector3(roomPosition.x, roomPosition.y - yOffset, 0)
            == rooms[roomNumber - 1].gameObject.transform.position);

        newRoom.UpdateRoom(xOffset, yOffset);
        int randomRoomType = Random.Range(0, wallType.wallList.Count);  //此处是随机选择墙壁种类
                                                                        //如果你不需要随机就改掉这个部分

        //根据房门选择房间

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


