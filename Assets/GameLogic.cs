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
            Vector3 loc = player.transform.position + new Vector3(rand.x, 0, rand.y) * 20;
            Instantiate(explodeAnime, loc, Quaternion.identity);
        }
    }

    public void EndGame() {
        Debug.Log("damm you died");
    }
}
