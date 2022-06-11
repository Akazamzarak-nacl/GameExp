using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //单击鼠标左键发射球体并在原地留下一个
        if (Input.GetMouseButtonDown(0))
        {
            GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            ball.transform.position = transform.position;
            ball.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            //更改球体的颜色为红色
            ball.GetComponent<Renderer>().material.color = Color.red;
            Rigidbody rb = ball.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.AddForce(Vector3.back * speed, ForceMode.Impulse);
        }
    }
}
