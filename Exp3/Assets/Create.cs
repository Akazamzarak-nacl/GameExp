using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    private void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);//生成立方体
            obj.transform.position = new Vector3(Random.Range(-5, 5), Random.Range(0, 2), Random.Range(-5, 5));//随机设置立方体位置
            obj.transform.localScale = new Vector3(Random.Range(0.5f, 2), Random.Range(0.5f, 2), Random.Range(0.5f, 2));//随机设置立方体大小
            obj.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));//随机设置立方体颜色
        }
    }

    int j = 0;
    GameObject obj;
    private void FixedUpdate()
    {
        if (j < 5)
        {
            obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.position = new Vector3(Random.Range(-5, 5), Random.Range(0, 2), Random.Range(-5, 5));
            obj.transform.localScale = new Vector3(Random.Range(0.5f, 2), Random.Range(0.5f, 2), Random.Range(0.5f, 2));
            obj.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            j++;
            GameObject.Find("Main Camera").transform.LookAt(new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z));//使相机对准新生成的物体


        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width - 150, 10, 100, 200), "名称：Cube" + j);
        GUI.Label(new Rect(Screen.width - 150, 25, 100, 200), "位置：(" + obj.transform.position.x + "," + obj.transform.position.y + "," + obj.transform.position.z + ")");
        GUI.Label(new Rect(Screen.width - 150, 40, 100, 200), "尺寸：" + obj.transform.localScale.x + ",\n" + obj.transform.localScale.y + ",\n" + obj.transform.localScale.z);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
