using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDrag : MonoBehaviour
{
    Vector3 initialPos;

    void Start()
    {

    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        //Debug.Log(mousePos.x+"-"+mousePos.y);
    }

    void OnMouseEnter()
    {
        //Debug.Log("mm");
        //Debug.Log(initialPos.x);

    }

    void OnMouseExit()
    {
        //Debug.Log("1mm");
        
    }

    void OnMouseDrag()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //Update();
        //Debug.Log("1mm");
        
    }
}
