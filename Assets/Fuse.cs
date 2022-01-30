using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class Fuse : MonoBehaviour
{
    public PickableItem dynamite;
    public VisualEffect fuse;
    bool lit = false;
    // Start is called before the first frame update
    void Start()
    {
        fuse.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(!lit)
        {
            fuse.Stop();
            if (dynamite.fuse)
            {
                lit = true;
                fuse.Play();
            }

        }
    }
}
