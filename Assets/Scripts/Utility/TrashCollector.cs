using System;
using UnityEngine;

public class TrashCollector : MonoBehaviour
{
    [SerializeField] private GameObject trashBag;
    
    BoxCollider grabBox;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grabBox = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            IGripTrash gripTrash;
            
            try
            {
                gripTrash = other.gameObject.GetComponent<IGripTrash>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            gripTrash.Action(trashBag);
        }
    }
}
