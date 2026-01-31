using System;
using UnityEngine;

public class TrashCollector : MonoBehaviour
{
    [SerializeField] private GameObject trashBag;
    
    private BoxCollider _grabBox;
    private IGripTrash gripTrash;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (_grabBox == null)
        {
            _grabBox = GetComponent<BoxCollider>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checkl for trash
        if (other.CompareTag("Trash"))
        {
            // Try to grab the IGripTrash Component on the trash object
            try
            {
                gripTrash = other.gameObject.GetComponent<IGripTrash>();
                gripTrash.Action(trashBag);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
