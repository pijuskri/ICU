using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blimp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 0.05f;
    }
}
