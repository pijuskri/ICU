using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public static GameLogic instance = null;

    public GameObject explodeAnime;
    public GameObject player;
    public Image fadeBack;
    public GameObject endBadScreen;
    public GameObject endGoodScreen;
    public GameObject warningCross;

    public float chanceToSpawnExplosion = 0.05f;
    public float fadeSpeed = 0.75f;
    public bool gameOutcome = false;
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
        //randomSpawn();
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
            GameObject warning = Instantiate(warningCross, new Vector3(loc.x, 4, loc.z), Quaternion.identity);
            Destroy(warning, 10f);
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
        player.transform.position = new Vector3(hit.point.x, player.transform.position.y, hit.point.z);
    }

    public void EndGame(bool outcome) {
        gameOutcome = outcome;
        if(outcome) Debug.Log("damm you won");
        else Debug.Log("damm you died");
        StartCoroutine(FadeImage(false, EndGameScreen));
    }

    IEnumerator FadeImage(bool fadeAway, Action doAfter)
    {
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime * fadeSpeed)
            {
                fadeBack.color = new Color(fadeBack.color.r, fadeBack.color.g, fadeBack.color.b, i);
                yield return null;
            }
        }
        else
        {
            for (float i = 0; i <= 1.7; i += Time.deltaTime * fadeSpeed)
            {
                fadeBack.color = new Color(fadeBack.color.r, fadeBack.color.g, fadeBack.color.b, i);
                yield return null;
            }
        }
        doAfter();
    }
    void EndGameScreen() {
        if(gameOutcome) endGoodScreen.SetActive(true);
        else endBadScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void StartGame() {
        endGoodScreen.SetActive(false);
        endBadScreen.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("test");
    }

   
}
