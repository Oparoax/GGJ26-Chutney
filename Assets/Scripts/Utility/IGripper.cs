using UnityEngine;

public class IGripper : MonoBehaviour
{
    [SerializeField] protected SphereCollider interactCollider;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (interactCollider == null)
        {
            Debug.LogError("ITrashable: Where is my interact collider, huh!");
        }

        StartChild();
    }
    
    // Pass through for start method?
    protected virtual void StartChild(){}

    // Inherited, to be overidden
    public virtual void Action(GameObject other) {}
}
