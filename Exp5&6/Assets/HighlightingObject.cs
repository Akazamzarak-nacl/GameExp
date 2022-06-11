using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightingObject : MonoBehaviour
{
    private bool isSelected = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && isSelected == true)
            {
                string output = "new position:" + hit.point.ToString();
                Debug.Log(output);
                gameObject.GetComponent<Transform>().position = hit.point;
            }
            if (Physics.Raycast(ray, out hit) && isSelected == false && hit.transform == transform)
            {
                isSelected = true;
                Debug.Log("object selected");
                gameObject.AddComponent<cakeslice.Outline>();
            }
        }
    }
}
