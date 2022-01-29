using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject explosion;
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosion, collision.GetContact(0).point, Quaternion.identity);
        Destroy(gameObject);
    }
}
