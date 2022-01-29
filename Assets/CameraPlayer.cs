using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        RenderPipelineManager.beginCameraRendering += BeginCameraRendering;
        RenderPipelineManager.endCameraRendering += EndCameraRendering;
    }

    float speed = 5f;
    float zoomSpeed = 0.5f;
    void Update()
    {
        int xmove = Convert.ToInt32(Input.GetKey(KeyCode.A)) - Convert.ToInt32(Input.GetKey(KeyCode.D));
        int ymove = Convert.ToInt32(Input.GetKey(KeyCode.W)) - Convert.ToInt32(Input.GetKey(KeyCode.S));
        int zoom = Convert.ToInt32(Input.GetKey(KeyCode.Q)) - Convert.ToInt32(Input.GetKey(KeyCode.E));
        transform.position += ((Vector3.left * xmove) + (Vector3.forward * ymove)) * Time.deltaTime * speed;
        transform.position += Vector3.up * zoom * Time.deltaTime * ZoomSpeed();
    }
    float ZoomSpeed() {
        return Mathf.Clamp(transform.position.y, 3, 20) * zoomSpeed;
    }
    void BeginCameraRendering(ScriptableRenderContext src, Camera cam) {
        if (GetComponent<Camera>() == cam) {
            RenderSettings.fog = false;
        }
    }
    void EndCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        if (GetComponent<Camera>() == camera)
        {
            RenderSettings.fog = true;
        }
        
    }
    void OnDestroy()
    {
        RenderPipelineManager.beginCameraRendering -= BeginCameraRendering;
        RenderPipelineManager.endCameraRendering -= EndCameraRendering;
    }

}
