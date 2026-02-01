using UnityEngine;

public class Swiper : MonoBehaviour
{
    Rigidbody _rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            var otherRB =  other.GetComponent<Rigidbody>();
            //otherRB.AddForce( ,ForceMode.Impulse);
        }
    }
}

