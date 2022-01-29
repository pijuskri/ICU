using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tripwire : MonoBehaviour
{
    public GameObject explodeAnime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject == GameLogic.instance.player)
        {
            Instantiate(explodeAnime, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
