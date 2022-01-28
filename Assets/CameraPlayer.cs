using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        
    }

    float speed = 5f;
    float zoomSpeed = 5f;
    void Update()
    {
        int xmove = Convert.ToInt32(Input.GetKey(KeyCode.A)) - Convert.ToInt32(Input.GetKey(KeyCode.D));
        int ymove = Convert.ToInt32(Input.GetKey(KeyCode.W)) - Convert.ToInt32(Input.GetKey(KeyCode.S));
        int zoom = Convert.ToInt32(Input.GetKey(KeyCode.Q)) - Convert.ToInt32(Input.GetKey(KeyCode.E));
        transform.position += ((Vector3.left * xmove) + (Vector3.forward * ymove) + (Vector3.up * zoom)) * Time.deltaTime * speed;
    }
}
