using UnityEngine;

public class IGripTrash : IGripper
{
    [SerializeField] [Range(0.1f,10.0f)] float grabSpeed = 0.2f;
    [SerializeField] [Range(0.1f,10.0f)] float grabScaleSpeed = 0.2f;
    [SerializeField] [Range(0.01f, 1.0f)] private float killDistanceThreshold = 0.1f;

    [SerializeField] private Vector3 finalScale;
    
    [SerializeField] SphereCollider worldCollider;

    [SerializeField] private float mass = 1f;
    
    private bool isGripped;
    private GameObject gripper;
    
    protected override void StartChild()
    {
        if (worldCollider == null)
        {
            worldCollider =  GetComponent<SphereCollider>();
        }
    }

    private void Update()
    {
        if (isGripped)
        {
            transform.position = Vector3.Slerp(transform.position, transform.TransformDirection(gripper.transform.position), grabSpeed);
            transform.localScale = Vector3.Slerp(transform.localScale, finalScale, grabScaleSpeed);

            if (Vector3.Distance(transform.position, gripper.transform.position) < killDistanceThreshold)
            {
                // Once close enough to the player send a ui signal and kill the object.
                
                // TODO: FIRE UI SIGNAL
                
                Destroy(gameObject);
            }
        }
    }

    public override void Action(Collider other)
    {
        isGripped = true;
        gripper = other.gameObject;
    }
}
