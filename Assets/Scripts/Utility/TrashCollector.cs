using System;
using System.Collections;
using UnityEngine;

public class TrashCollector : MonoBehaviour
{
    [SerializeField] private GameObject trashBagOnModel;
    
    [SerializeField] private GameObject trashBagPlacedPrefab;
    [SerializeField] private Transform trashBagSpawn;
    [SerializeField] private GameObject trashBagThrownPrefab;
    
    [SerializeField] private GameObject trashParent;
    
    [SerializeField] private float grabWindow = 0.3f;
    
    private BoxCollider _grabBox;
    
    private GripAndThrowable gripTarget;

    private bool isGripping = true;
    private bool isGrippingTrashBag = true;
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
            Debug.Log("Grabbing");
            StartCoroutine(waitForGrab(grabWindow));
        }
        else if (isGripping)
        {
            Debug.Log("Drop");

            if (gripTarget != null)
            {
                gripTarget.Action(null);
            }
            
            isGripping = false;

            if (isGrippingTrashBag)
            {
                Debug.Log("Dropping trash bag");
                trashBagOnModel.SetActive(false);
                Instantiate(trashBagPlacedPrefab, trashBagSpawn.position, Quaternion.identity);
                isGrippingTrashBag = false;
            }
            
        }
    }

    public void Throw(Vector3 throwDirection, float throwForce)
    {
        if (isGripping)
        {
            gripTarget.Throw(throwDirection, throwForce);
            isGripping = false;
            
            if (isGrippingTrashBag)
            {
                trashBagOnModel.SetActive(false);
                var trashBag = Instantiate(trashBagThrownPrefab, trashBagSpawn.position, Quaternion.identity);

                try
                {
                    var trashBagRB =  trashBag.GetComponent<Rigidbody>();
                    trashBagRB.AddForce(throwDirection * throwForce);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
                isGrippingTrashBag = false;
            }
        }
    }

    private IEnumerator waitForGrab(float seconds)
    {
        canGrab = true;
        Debug.Log("Grabbing Window open");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Grabbing Window closed");
        canGrab = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!canGrab)
        {
            Debug.Log("Can't Grab");
            return;
        }
        
        // Check for trash
        if (other.CompareTag("Trash"))
        {
            // Try to grab the IGripTrash Component on the trash object
            try
            {
                Debug.Log("Grabbing Trash");
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
                Debug.Log("Grabbing Trash Bag");
                gripTarget = other.gameObject.GetComponent<IGripTrashBag>();
                gripTarget.Action(null);
                trashBagOnModel.SetActive(true);

                isGrippingTrashBag = true;
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
