using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxPickup : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject guide;
    void Start()
    {
        guide.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void showGuide() {
        guide.SetActive(true);
        Debug.Log("shown");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameLogic.instance.player)
        {
            GameLogic.instance.PickAmmo();
            Destroy(gameObject);
        }
    }
}
