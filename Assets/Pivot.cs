using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivot : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = target.transform.rotation;
        transform.position = new Vector3(target.transform.position.x, -0.4f, target.transform.position.z);
    }
}
