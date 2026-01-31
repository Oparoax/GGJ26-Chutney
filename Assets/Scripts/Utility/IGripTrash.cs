using UnityEngine;

public class IGripTrash : GripAndThrowable
{
    [SerializeField] [Range(0.01f, 1.0f)] private float grabSpeed = 0.2f;
    [SerializeField] [Range(0.01f, 1.0f)] private float grabScaleSpeed = 0.2f;
    //[SerializeField] [Range(0.01f, 1.0f)] private float killDistanceThreshold = 0.1f;
    
    [SerializeField] [Range(0.1f, 2.0f)] private float maxTime = 0.7f;

    [SerializeField] private Vector3 finalScale;

    [SerializeField] private SphereCollider modelCollider;

    [SerializeField] private float mass = 1f;
    
    private bool _isCollected;
    private bool _isGrabbed;
    
    private GameObject _collector;
    private GameObject _parent;

    private float timeAtGrab;

    private void Update()
    {
        if (_isCollected)
        {
            // Lerp scale and position towards the player.
            transform.position = Vector3.Lerp(transform.position, transform.TransformDirection(_collector.transform.position), grabSpeed);
            transform.localScale = Vector3.Lerp(transform.localScale, finalScale, grabScaleSpeed);

            // Trash will despawn after a set timer.
            if (Time.time - timeAtGrab > maxTime)
            {
                // Once close enough to the player send a ui signal and kill the object.
                
                // TODO: FIRE UI SIGNAL
                
                _isCollected = false;
                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (_isGrabbed)
        {
            return;
        }
        
        if (other.CompareTag("TrashBag"))
        {
            // Activate loop
            _isCollected = true;
        
            // Get Player for location data
            _collector = other.gameObject;

            // Disable collisions & physics
            interactCollider.enabled = false;
            modelCollider.enabled = false;
        
            Rb.isKinematic = true;

            // Grab time at grab for timer
            timeAtGrab = Time.time;
        }
    }

    public override void Action(GameObject parent)
    {
        if (!_isGrabbed)
        {
            ConnectToParent(parent);
        }
        else
        {
            DisconnectFromParent();
        }
        
    }
    
    public override void Throw(Vector3 throwDirection, float throwForce)
    {
        DisconnectFromParent();
        
        base.Throw(throwDirection, throwForce);
    }

    private void ConnectToParent(GameObject parent)
    {
        // Grab object
        _isGrabbed = true;
        
        // Get Player for location data
        _parent = parent;
            
        // Disable collisions & physics
        interactCollider.enabled = false;
        modelCollider.enabled = false;
        
        Rb.isKinematic = true; 
            
        // Snap trash to the location of the racoons hands.
        gameObject.transform.SetParent(_parent.transform);
        gameObject.transform.localPosition = Vector3.zero;
    }

    private void DisconnectFromParent()
    {
        // Drop Object
        _isGrabbed = false;
            
        interactCollider.enabled = true;
        modelCollider.enabled = true;
        
        Rb.isKinematic = false; 
            
        gameObject.transform.SetParent(null);
            
        _parent = null;
    }
}
