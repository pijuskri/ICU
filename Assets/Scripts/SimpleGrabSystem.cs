using UnityEngine;


public class SimpleGrabSystem : MonoBehaviour {
    // Reference to the character camera.
    [SerializeField]
    private GameObject characterCamera;
    // Reference to the slot for holding picked item.
    [SerializeField]
    private Transform slot;
    // Reference to the currently held item.
    private PickableItem _pickedItem;
    private Vector3 OGscale;

    /// <summary>
    /// Method called very frame.
    /// </summary>
    private void Update() {
        // Execute logic only on button pressed
        if (Input.GetButtonDown("Fire1"))
        {
            // Check if player picked some item already
            if (_pickedItem)
            {
                // If yes, drop picked item
                DropItem(_pickedItem);
            }
            else
            {
                // If no, try to pick item in front of the player
                // Create ray from center of the screen
                Vector3 origin = characterCamera.transform.position;
                Vector3 direction = characterCamera.transform.forward;
                RaycastHit hit;
                // Shot ray to find object to pick
                if (Physics.Raycast(origin, direction, out hit, 3f))
                {
                    // Check if object is pickable
                    var pickable = hit.transform.GetComponent<PickableItem>();
                    // If object has PickableItem class
                    if (pickable && !pickable.fuse)
                    {
                        // Pick it
                        PickItem(pickable);
                    }
                }
            }
        }

        if (Input.GetButtonDown("Fire2") && _pickedItem)
        {
            ThrowItem(_pickedItem);
        }
    }
    
    /// <summary>
    /// Method for picking up item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void PickItem(PickableItem item) {
        // Assign reference
        _pickedItem = item;
        // Disable rigidbody and reset velocities
        item.Rb.isKinematic = true;
        item.Rb.velocity = Vector3.zero;
        item.Rb.angularVelocity = Vector3.zero;
        OGscale = item.transform.localScale;
        // Set Slot as a parent
        item.transform.SetParent(slot, true);
        // Reset position and rotation
        item.transform.position = slot.transform.position;
        item.transform.eulerAngles = slot.transform.eulerAngles;
        Debug.Log(item.transform.localScale);
    }
    
    /// <summary>
    /// Method for dropping item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void DropItem(PickableItem item) {
        // Remove reference
        _pickedItem = null;
        // Remove parent
        item.transform.SetParent(null, true);
        // Reset to original scale
        item.transform.localScale = OGscale;
        // Enable rigidbody
        item.Rb.isKinematic = false;
        // Add force to throw item a little bit
        item.Rb.AddForce(item.transform.forward * 2, ForceMode.VelocityChange);
    }

    /// <summary>
    /// Method for throwing item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void ThrowItem(PickableItem item) {
        // Remove reference
        _pickedItem = null;
        // Remove parent
        item.transform.SetParent(null, true);
        // Reset to original scale
        item.transform.localScale = OGscale;
        // Enable rigidbody
        item.Rb.isKinematic = false;
        // Add force to throw item a little bit
        item.Rb.AddForce(item.transform.forward * 10, ForceMode.VelocityChange);

        item.fuse = true;
        item.timeRemaining = 5;
    }
}