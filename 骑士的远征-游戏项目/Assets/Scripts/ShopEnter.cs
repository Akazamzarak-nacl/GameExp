using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEnter : MonoBehaviour
{
    public GameObject shop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Time.timeScale = 0;
        GameObject instance = (GameObject)Instantiate(shop, transform.position, transform.rotation);
    }
}
