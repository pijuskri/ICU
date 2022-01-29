using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class FirstPerson : MonoBehaviour
{
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
    
    }
    float speed = 5f;
    // Update is called once per frame
    void Update()
    {
        int xmove = Convert.ToInt32(Input.GetKey(KeyCode.LeftArrow)) - Convert.ToInt32(Input.GetKey(KeyCode.RightArrow));
        int ymove = Convert.ToInt32(Input.GetKey(KeyCode.UpArrow)) - Convert.ToInt32(Input.GetKey(KeyCode.DownArrow));
        transform.position += ((-transform.right * xmove) + (transform.forward * ymove)) * Time.deltaTime * speed;
        Look();
    }

    public float lookSpeed = 3;
    private Vector2 rotation = Vector2.zero;
    public void Look() // Look rotation (UP down is Camera) (Left right is Transform rotation)
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
        transform.eulerAngles = new Vector2(0, rotation.y) * lookSpeed;
        camera.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);
    }
}
