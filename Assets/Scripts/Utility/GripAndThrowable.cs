using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GripAndThrowable : IGripper
{
    protected Rigidbody Rb;
    
    private void Start()
    {
        Rb =  GetComponent<Rigidbody>();
    }

    public virtual void Throw(Vector3 throwDirection, float throwForce)
    {
        Rb.AddForce(throwDirection.normalized * throwForce, ForceMode.Impulse);
    }
}
