using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSphereScale : MonoBehaviour
{
    public void changeSphereScale()
    {
        GameObject camera = GameObject.Find("Main Camera");
        Slider slider = GameObject.Find("Slider").GetComponent<Slider>();
        camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, -264 + slider.value);
    }
}
