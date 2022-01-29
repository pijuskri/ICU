using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PickableItem : MonoBehaviour
{
    // Reference to the rigidbody
    private Rigidbody _rb;
    public Rigidbody Rb => _rb;

    public bool fuse = false;
    public float timeRemaining;
    
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
}