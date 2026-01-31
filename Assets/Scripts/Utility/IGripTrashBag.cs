using UnityEngine;

public class IGripTrashBag : GripAndThrowable
{
    private bool _isGrabbed;
    
    public override void Action(GameObject parent)
    {
        Debug.Log("Destroy trash bag");
        _isGrabbed = true;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            other.gameObject.GetComponent<IGripTrash>().Collected(transform);
        }
    }
}
