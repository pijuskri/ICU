using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tripwire : MonoBehaviour
{
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
            Explode();
            if (pickable != null) {
                if (pickable.explodeAble) pickable.SetToExplode(0);
                else Destroy(pickable.gameObject);
            }
        }
    }
    public void Explode() {
        Instantiate(GameLogic.instance.explodeAnime, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
