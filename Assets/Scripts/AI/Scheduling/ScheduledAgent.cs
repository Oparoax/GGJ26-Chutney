using UnityEngine;

public class ScheduledAgent : MonoBehaviour
{
    [SerializeField] private string m_agentName;
    protected float m_movementSpeed;
    [SerializeField] protected GameObject[] m_ownedObjects;
    protected Schedule m_currentBehaviour;
    [SerializeField] protected OwnableObject m_target;
    protected Rigidbody rb;
    private int m_sequence;
    private bool m_acting;
    public GameObject[] OwnedObjects => m_ownedObjects;
    public string Name => m_agentName;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (m_currentBehaviour != null)
            m_currentBehaviour(m_target);
    }


    public void UpdateSchedules(Schedule nextAction)
    {
        m_currentBehaviour = nextAction;
        m_acting = true;
    }

    public void MoveTo(OwnableObject target)
    {
        Debug.Log($"Running MoveTo Action from {m_agentName}");
    }

    public void EnableDisable(OwnableObject target)
    {

    }
}
