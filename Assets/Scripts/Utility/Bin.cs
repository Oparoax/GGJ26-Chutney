using UnityEngine;

public class Bin : MonoBehaviour
{
    [SerializeField] private int trashAmount;
    [SerializeField] private float trashForce;
    [SerializeField] private Transform trashExplodeDirection;
    
    [SerializeField] private GameObject[] trashPrefabs;
    [SerializeField] private GameObject binLid;
        
    private Rigidbody rb;
    private bool hasExploded;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    public void Explode()
    {
        if (!hasExploded)
        {
            Debug.Log("Explode");
            
            hasExploded = true;
            
            binLid.SetActive(false);
        
            for (int i = 0; i < trashAmount; i++)
            {
                var trashToBeSent = trashPrefabs[Random.Range(0, trashPrefabs.Length - 1)];
                var trash = Instantiate(trashToBeSent, trashExplodeDirection.position, trashExplodeDirection.rotation);
                var trashRb = trash.GetComponent<Rigidbody>();
            
                trashRb.AddForce(trashExplodeDirection.transform.forward * trashForce, ForceMode.Impulse);
            }
        }
    }

    
}
