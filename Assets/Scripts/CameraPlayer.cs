using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    Camera camera;
    void Start()
    {
        camera = GetComponent<Camera>();
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
        transform.position += ((Vector3.left * xmove) + (Vector3.forward * ymove)) * Time.deltaTime * ZoomSpeed() * 0.75f;
        //transform.position += Vector3.up * zoom * Time.deltaTime * ZoomSpeed() * 0.75f;
        camera.orthographicSize = Mathf.Clamp( camera.orthographicSize + (zoom* ZoomSpeed() * 0.005f), 3, 50);
    }
    float ZoomSpeed()
    {
        return Mathf.Clamp(camera.orthographicSize, 5, 30);
    }
    void BeginCameraRendering(ScriptableRenderContext src, Camera cam) {
        if (camera == cam) {
            RenderSettings.fog = false;
        }
    }
    void EndCameraRendering(ScriptableRenderContext context, Camera cam)
    {
        if (camera == cam)
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
