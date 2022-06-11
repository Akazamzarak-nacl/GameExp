using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalButtonSelected : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<UnityEngine.UI.Button>().Select();//初始化选中普通模式
    }

    
}
