using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explodeAnime;
    public GameObject player;
    public static GameLogic instance = null;
    public float chanceToSpawnExplosion = 0.05f;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        randomSpawn();
    }

    float secondTimer = 0;
    void Update()
    {
        secondTimer += Time.deltaTime;
        if (secondTimer > 1) {
            secondTimer = 1f - secondTimer;
            spawnExplosion();
        }
    }

    void spawnExplosion() {
        if (Random.Range(0f, 1) < chanceToSpawnExplosion) {
            Vector2 rand = Random.insideUnitCircle;
            Vector3 loc = player.transform.position + new Vector3(rand.x, 0, rand.y) * 40;
            Instantiate(explodeAnime, loc, Quaternion.identity);
        }
    }

    void randomSpawn() {
        RaycastHit hit;
        int layerMask = 0;
        layerMask = ~layerMask;
        Vector2 rand = Random.insideUnitCircle;
        Vector3 origin = new Vector3(rand.x, 20, rand.y) * 30;
        // Does the ray intersect any objects excluding the player layer
        while (!(Physics.Raycast(origin, Vector3.down, out hit, Mathf.Infinity, layerMask) && hit.collider.CompareTag("Ground")))
        {
            rand = Random.insideUnitCircle;
            origin = new Vector3(rand.x, 0, rand.y) * 30;
        }
        Debug.DrawRay(origin, Vector3.down * hit.distance, Color.yellow);
        player.transform.position = new Vector3(hit.point.x, player.transform.position.y, hit.point.z);
        Debug.Log("Did Hit");
    }

    public void EndGame() {
        Debug.Log("damm you died");
    }
}
