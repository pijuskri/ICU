using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeToDestroy = 10;
    public float killRange = 5;
    GameObject player;
    void Start()
    {
        player = GameLogic.instance.player;
        Destroy(gameObject, timeToDestroy);
        if (Vector3.Distance(player.transform.position, transform.position) < killRange) {
            
        }
        RaycastHit hit;
        Vector3 dir = (GameLogic.instance.player.transform.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, dir, out hit, killRange))
        {
           if(hit.collider.gameObject == GameLogic.instance.player) 
            GameLogic.instance.EndGame(false);
        }
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}
