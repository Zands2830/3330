using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviors : MonoBehaviour
{
    public float dragSpeed = 40f;
    public float rotateSpeed = 40f;
    public float zoomSpeed = 10f;
    public Vector3 dragOrigin;
    public bool isMouseMoving = false;
    public Vector3 lastMousePosition;

    void Start()
    {

    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            MouseStuff();
        }

        //Performs camera y shifts
        Vector3 rise = new Vector3(0, Input.GetAxis("Mouse ScrollWheel"), 0);
        rise *= zoomSpeed;
        transform.Translate(-rise, Space.World);
    }

    private void MouseStuff()
    {
        if (Input.mousePosition != lastMousePosition)
        {
            lastMousePosition = Input.mousePosition;
            isMouseMoving = true;
        }
        else { isMouseMoving = false; }

        if (isMouseMoving == true)
        {
            if (Input.GetKey("left shift"))
            {
                //transform.eulerAngles += rotateSpeed * new Vector3(0, Input.GetAxis("Mouse X"), 0);
            }
            else
            {
                transform.position += dragSpeed * new Vector3(-Input.GetAxis("Mouse X"), 0, -Input.GetAxis("Mouse Y"));
                dragOrigin = Input.mousePosition;
            }
        }
    }
}
