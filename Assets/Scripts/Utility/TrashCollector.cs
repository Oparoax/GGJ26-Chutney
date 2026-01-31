using System;
using System.Collections;
using UnityEngine;

public class TrashCollector : MonoBehaviour
{
    [SerializeField] private GameObject trashBagParent;
    [SerializeField] private GameObject trashParent;
    
    [SerializeField] private float grabWindow = 0.3f;
    
    private BoxCollider _grabBox;
    
    private IGripper gripTarget;

    private bool isGripping;
    private bool canGrab;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_grabBox == null)
        {
            _grabBox = GetComponent<BoxCollider>();
        }
    }

    public void Grab()
    {
        if (!isGripping)
        {
            StartCoroutine(waitForGrab(grabWindow));
        }
        else if (isGripping)
        {
            Debug.Log("Drop");
            gripTarget.Action(null);
            isGripping = false;
        }
    }


    private IEnumerator waitForGrab(float seconds)
    {
        canGrab = true;

        yield return new WaitForSeconds(seconds);
        
        canGrab = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!canGrab)
        {
            return;
        }
        
        // Check for trash
        if (other.CompareTag("Trash"))
        {
            // Try to grab the IGripTrash Component on the trash object
            try
            {
                gripTarget = other.gameObject.GetComponent<IGripTrash>();
                gripTarget.Action(trashParent);
                
                isGripping = true;
                canGrab = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        else if (other.CompareTag("TrashBag"))
        {
            try
            {
                gripTarget = other.gameObject.GetComponent<IGripTrashBag>();
                gripTarget.Action(trashBagParent);
                isGripping = true;
                canGrab = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
