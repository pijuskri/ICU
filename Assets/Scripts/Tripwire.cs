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
        var pickable = other.gameObject.GetComponent<PickableItem>();
        if (other.gameObject == GameLogic.instance.player || pickable != null)
        {
            Instantiate(explodeAnime, transform.position, Quaternion.identity);
            Destroy(gameObject);
            if (pickable != null) {
                if (pickable.explodeAble) pickable.SetToExplode(0);
                else Destroy(pickable.gameObject);
            }
        }
    }
}
