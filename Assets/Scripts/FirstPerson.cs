using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FirstPerson : MonoBehaviour
{
    public Camera camera;
    private Rigidbody rgb;
    private float maxHeight = 3;
    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody>();
    }
    float speed = 5f;
    // Update is called once per frame
    void Update()
    {
        int xmove = Convert.ToInt32(Input.GetKey(KeyCode.LeftArrow)) - Convert.ToInt32(Input.GetKey(KeyCode.RightArrow));
        int ymove = Convert.ToInt32(Input.GetKey(KeyCode.UpArrow)) - Convert.ToInt32(Input.GetKey(KeyCode.DownArrow));
        transform.position += ((-transform.right * xmove) + (transform.forward * ymove)) * Time.deltaTime * speed;
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -2, maxHeight), transform.position.z);
        rgb.velocity = new Vector3(0,rgb.velocity.y,0);
        Look();
    }

    public float lookSpeed = 3;
    private Vector2 rotation = Vector2.zero;
    public void Look()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
        transform.eulerAngles = new Vector2(0, rotation.y) * lookSpeed;
        camera.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);
    }

    // used fields
    [SerializeField] private float cameraShakeDuration = 1f;
    [SerializeField] private float cameraShakeDecreaseFactor = 1f;
    [SerializeField] private float cameraShakeAmount = 0.3f;
    // coroutine
    public IEnumerator ShakeCamera(float strength)
    {
        var originalPos = camera.transform.localPosition;
        var duration = cameraShakeDuration * strength;
        var shakeReduction = 1f;
        while (duration > 0)
        {
            shakeReduction = duration > 0 ? duration / cameraShakeDuration : 0;
            camera.transform.localPosition = originalPos + Random.insideUnitSphere * cameraShakeAmount * strength * shakeReduction;
            duration -= Time.deltaTime * cameraShakeDecreaseFactor;
            yield return null;
        }
        camera.transform.localPosition = originalPos;

        /*
        if (shakeDuration > 0)
        {
            gameObject.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
            shakeAmount -= Time.deltaTime * decreaseFactor;
            if (shakeAmount <= 0) shakeAmount = 0;
        }
        else
        {
            shakeDuration = 0f;
            gameObject.transform.localPosition = originalPos;
        }
        */
    }



}
