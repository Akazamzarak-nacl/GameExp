using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public Animator animator;
    public GameObject robot;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetTrigger("Wark");
            robot.transform.rotation = Quaternion.Euler(0, 0, 0);
            robot.transform.Translate(Vector3.forward * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetTrigger("Wark");
            robot.transform.rotation = Quaternion.Euler(0, 180, 0);
            robot.transform.Translate(Vector3.forward * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetTrigger("Wark");
            robot.transform.rotation = Quaternion.Euler(0, -90, 0);
            robot.transform.Translate(Vector3.forward * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetTrigger("Wark");
            robot.transform.rotation = Quaternion.Euler(0, 90, 0);
            robot.transform.Translate(Vector3.forward * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("Dead");
        }
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            animator.SetTrigger("Attack");
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.U) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            animator.ResetTrigger("Wark");
            animator.ResetTrigger("Jump");
            animator.SetTrigger("Idle");
        }
    }
}
