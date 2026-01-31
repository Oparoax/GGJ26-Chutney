using UnityEngine;

public class ITrashable : MonoBehaviour
{
    [SerializeField] private SphereCollider interactCollider;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (interactCollider == null)
        {
            Debug.LogError("ITrashable: Where is my interact collider, huh!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Action(other);
        }
    }

    // Inherited, to be overidden
    protected void Action(Collider other) {}
}
