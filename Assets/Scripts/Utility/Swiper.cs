using System.Collections;
using UnityEngine;

public class Swiper : MonoBehaviour
{
    [SerializeField] private float LaunchForce = 5f;
    [SerializeField] private float SwipeDuration = 0.5f;
    
    private Rigidbody _rb;
    private bool canSwipe;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody>();
        }
    }

    public void Swipe()
    {
        StartCoroutine(SwipeTime(SwipeDuration));
    }

    private IEnumerator SwipeTime(float duration)
    {
        canSwipe = true;
        
        yield return new WaitForSeconds(duration);
        
        canSwipe = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (canSwipe)
        {
            if (other.CompareTag("Trash"))
            {
                Debug.Log("Trash Swiped");
                var otherRb =  other.GetComponent<Rigidbody>();
                var launchDir = other.gameObject.transform.position - gameObject.transform.position;
            
                otherRb.AddForce(launchDir.normalized *  LaunchForce,ForceMode.Impulse);
            }
            else if (other.CompareTag("Bin"))
            {
                Debug.Log("Bin-Splode");
                var bin = other.GetComponent<Bin>();
                bin.Explode();
            }
        }
    }
}

