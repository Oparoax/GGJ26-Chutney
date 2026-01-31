using UnityEngine;

public class TrashInFlight : MonoBehaviour
{
    [SerializeField] private GameObject _placedTrashBag;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            Instantiate(_placedTrashBag, transform.position, Quaternion.identity);
        }
    }
}
