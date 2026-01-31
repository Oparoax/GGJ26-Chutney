using UnityEngine;

public class IGripTrash : IGripper
{
    [SerializeField] [Range(0.01f, 1.0f)] private float grabSpeed = 0.2f;
    [SerializeField] [Range(0.01f, 1.0f)] private float grabScaleSpeed = 0.2f;
    //[SerializeField] [Range(0.01f, 1.0f)] private float killDistanceThreshold = 0.1f;
    
    [SerializeField] [Range(0.1f, 2.0f)] private float maxTime = 0.7f;

    [SerializeField] private Vector3 finalScale;

    [SerializeField] private SphereCollider modelCollider;
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float mass = 1f;
    
    private bool _isGripped;
    private GameObject _gripper;

    private float timeAtGrab;

    private void Update()
    {
        if (_isGripped)
        {
            // Lerp scale and position towards the player.
            transform.position = Vector3.Lerp(transform.position, transform.TransformDirection(_gripper.transform.position), grabSpeed);
            transform.localScale = Vector3.Lerp(transform.localScale, finalScale, grabScaleSpeed);

            // Trash will despawn after a set timer.
            if (Time.time - timeAtGrab > maxTime)
            {
                // Once close enough to the player send a ui signal and kill the object.
                
                // TODO: FIRE UI SIGNAL
                
                _isGripped = false;
                Destroy(gameObject);
            }
        }
    }

    public override void Action(GameObject player)
    {
        // Activate loop
        _isGripped = true;
        
        // Get Player for location data
        _gripper = player;

        // Disable collisions & physics
        interactCollider.enabled = false;
        modelCollider.enabled = false;
        
        _rb.isKinematic = true;

        // Grab time at grab for timer
        timeAtGrab = Time.time;
    }
}
