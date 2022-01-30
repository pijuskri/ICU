using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PickableItem : MonoBehaviour
{
    // Reference to the rigidbody
    private Rigidbody _rb;
    public Rigidbody Rb => _rb;

    public bool explodeAble = false;
    public bool fuse = false;
    public float fuseTime = 6;
    float timeRemaining;
    
    /// <summary>
    /// Method called on initialization.
    /// </summary>
    private void Awake()
    {
        // Get reference to the rigidbody
        _rb = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        if (fuse)
            if (timeRemaining > 0)
                timeRemaining -= Time.deltaTime;
            else
            {
                Instantiate(GameLogic.instance.explodeAnime, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
    }
    public void SetToExplode(float time)
    {
        fuse = true;
        timeRemaining = time;
    }
    public void SetToExplode()
    {
        fuse = true;
        timeRemaining = fuseTime;
    }
}