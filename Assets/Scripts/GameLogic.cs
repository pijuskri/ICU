using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using System.Linq;


public class GameLogic : MonoBehaviour
{
    public static GameLogic instance = null;

    public GameObject explodeAnime;
    public GameObject player;
    public Image fadeBack;
    public GameObject endBadScreen;
    public GameObject endGoodScreen;
    public GameObject warningCross;
    public Tilemap tilemap;
    public AmmoBoxPickup[] ammoBoxes;
    //55, -23, 87, 8
    public Vector4 playableArea = new Vector4(-20, 50, 12, 83);
    public float chanceToSpawnExplosion = 0.075f;
    public float fadeSpeed = 0.75f;
    public bool gameOutcome = false;
    public int ammoCollected = 0;
    public int totalAmmo = 5;
    float shellHeight = 50;
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
        NextGuide();
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
            Vector3 loc = RandomPointInArea(shellHeight);
            Instantiate(explodeAnime, loc, Quaternion.identity);
            GameObject warning = Instantiate(warningCross, new Vector3(loc.x, 6, loc.z), Quaternion.Euler(Vector3.right*90));
            Destroy(warning, 10f);
        }
    }
    Vector3 RandomPointInArea(float y) {
        return new Vector3(Random.Range(playableArea.x, playableArea.y), y, Random.Range(playableArea.z, playableArea.w));
    }

    void randomSpawn() {
        /*
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
        */
        Vector3Int cellPosition = tilemap.WorldToCell(RandomPointInArea(0));
        Vector3 loc = tilemap.GetCellCenterWorld(cellPosition);
        Debug.Log(RandomPointInArea(0));
        player.transform.position = new Vector3(loc.x, player.transform.position.y + 1, loc.z);
    }

    public void PickAmmo() {
        ammoCollected++;
        ammoBoxes = ammoBoxes.Where(x => x != null).ToArray();
        NextGuide();
    }
    void NextGuide() {
        if (ammoBoxes.Length > 0) ammoBoxes[0].showGuide();
    }

    public void EndGame(bool outcome) {
        gameOutcome = outcome;
        if (outcome) {
            if (ammoCollected >= totalAmmo) StartCoroutine(FadeImage(false, EndGameScreen));
        }
        else StartCoroutine(FadeImage(false, EndGameScreen));

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
        SceneManager.LoadScene("Level");
    }

   
}
