using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject explosion;
    float time = 0;
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosion, collision.GetContact(0).point, Quaternion.identity);
        Debug.Log(time);
        Destroy(transform.parent.gameObject);
    }
	private void Update()
	{
        time += Time.deltaTime;
	}
}
