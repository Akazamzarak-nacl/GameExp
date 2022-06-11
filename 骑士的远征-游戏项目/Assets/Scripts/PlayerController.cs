using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于控制角色移动
/// 负责2的同学要修改这个

/// 负责2的同学收到！
/// </summary>

public class PlayerController : MonoBehaviour
{

    [Header("控制角色移动")]
    Rigidbody2D rb; //���rigidbody2d
    Animator anim;  //��ö���

    Vector2 movement;   //�ƶ�
    public float speed; //�ƶ��ٶ�
    private float dodgeSpeed; //����ٶ� 
    private Vector2 dodgeDir; //��ܷ���

    //枪械
    public GameObject[] weapons;
    public static List<int> weaponNum;
    public  List<GameObject> guns;
    public static int gunNum;
    private enum State
    {
        Normal,
        Dodging,
    }
    private State state; //״̬

    public static PlayerController instance { get; private set; }

    /*生命ֵ*/
    public int maxHealth = 20;//最大生命
    private static int currentHealth;//当前生命

    /*金币*/
    private static int coinCount = 0;//金币数

    /*游戏结束界面*/
     public GameObject gameoverMenu;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        state = State.Normal;

        instance = this;

        currentHealth = maxHealth;

        UIManager.instance.UpdateHealthBar(currentHealth, maxHealth);
        UIManager.instance.UpdateCoinCount(coinCount);

       if( weaponNum ==null||weaponNum.Count==0)
        {
            weaponNum = new List<int>();
            weaponNum.Add(0);
            Debug.Log(weaponNum.Count);
        }
        //激活第一个枪械
            for(int i=0;i<weaponNum.Count;i++)
                guns.Add(weapons[weaponNum[i]]);
            guns[gunNum].SetActive(true);
            Debug.Log(guns.Count);

    }

    // Update is called once per frame
    void Update()
    {
        
        switch (state)
        {
            case State.Normal:
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");

                //�����ƶ�ʱ�ı�����ĳ���
                if (movement.x != 0)
                {
                    transform.localScale = new Vector3(movement.x * 2, 2, 1); //�˴�ֵ��Ϊ��ƥ�������scale������
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    dodgeDir = movement;
                    dodgeSpeed = 30f;
                    state = State.Dodging;
                }

                //每一帧都要调用动画变换函数
                SwithAnim();
                SwitchGun();
                break;
            case State.Dodging:
                float dodgeSpeedDropMulpitier = 5f;
                dodgeSpeed -= dodgeSpeed * dodgeSpeedDropMulpitier * Time.deltaTime;
                if (movement.x != 0)
                {
                    transform.localScale = new Vector3(movement.x * 2, 2, 1); //�˴�ֵ��Ϊ��ƥ�������scale������
                }
                anim.SetFloat("dodgeSpeed", dodgeSpeed);


                float dodgeSpeedMinimum = 10f;
                if (dodgeSpeed < dodgeSpeedMinimum)
                {
                    state = State.Normal;
                }
                break;
        }
        //�ƶ�

    }


    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:
                rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);    //完成移动
                break;
            case State.Dodging:
                rb.velocity = dodgeDir * dodgeSpeed;
                break;
        }


    }

    void SwithAnim() //切换动画
    {
        anim.SetFloat("speed", movement.magnitude); //通过改变speed的值从静止动作切换到移动动作
    }
    void SwitchGun()
    {
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(guns.Count);
            Debug.Log(gunNum);
            guns[gunNum].SetActive(false);
            if (--gunNum < 0)
            {
                gunNum = guns.Count-1;
            }
            guns[gunNum].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            guns[gunNum].SetActive(false);
            if (++gunNum > guns.Count - 1)
            {
                gunNum = 0;
            }
            guns[gunNum].SetActive(true);
        }
    }
    public void ChangeHealth(int amount)//amount为改变的血量
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIManager.instance.UpdateHealthBar(currentHealth, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
        /*死亡*/
        if (currentHealth <= 0)
        {
            Time.timeScale = 0;
            gameoverMenu.SetActive(true);
        }
    }

    public void ChangeCoinCount(int amount)//amount为金币该变量
    {
        coinCount = Mathf.Clamp(coinCount + amount, 0, 10000);
        UIManager.instance.UpdateCoinCount(coinCount);
    }
    public int getCurrentHealth()
    {
        // Debug.Log("getCurrentHealth()");
        return currentHealth;

    }
    public int getCurrentCoin()
    {
        
        return coinCount;

    }
    public void setStartCurrentHealth()
    {
        currentHealth = maxHealth;
    }

    public void setStartCoinCount()
    {
        coinCount = 0;
    }
}
