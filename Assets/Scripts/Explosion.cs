using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeToDestroy = 4;
    public float killRange = 5;
    public float maxShakeDistance = 40;
    GameObject player;
    FirstPerson firstPerson;
    void Start()
    {
        player = GameLogic.instance.player;
        firstPerson = player.GetComponentInChildren<FirstPerson>();
        Destroy(gameObject, timeToDestroy);

        shake();

        RaycastHit hit;
        Vector3 dir = (GameLogic.instance.player.transform.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, dir, out hit, killRange))
        {
           if(hit.collider.gameObject == GameLogic.instance.player) 
            GameLogic.instance.EndGame(false);
        }
       
    }
    void shake() {
        float strength = Mathf.Clamp(maxShakeDistance / Vector3.Distance(player.transform.position, transform.position), 0, 1.5f);
        StartCoroutine(firstPerson.ShakeCamera(strength));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        var trip = other.gameObject.GetComponent<Tripwire>();
        if (trip != null)
        {
            trip.Explode();
        }
    }
}
