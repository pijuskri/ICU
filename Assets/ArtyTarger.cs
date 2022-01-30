using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtyTarger : MonoBehaviour
{
    // Start is called before the first frame update
    float timer = GameLogic.timeForShellLand;
    Vector3 scale;
    void Start()
    {
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        Vector3 newScale = scale * Mathf.Clamp(timer / GameLogic.timeForShellLand, 0.1f, 1);
        transform.localScale = newScale;
    }
}
