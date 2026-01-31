using UnityEngine;

public class ScheduledAgent : AgentBase
{
    [SerializeField] private string m_agentName;
    protected float m_movementSpeed;
    [SerializeField] protected GameObject[] m_ownedObjects;
    protected Schedule m_currentBehaviour;
    [SerializeField] protected OwnableObject m_target;
    private bool m_acting;
    public bool IsActing => m_acting;
    public GameObject[] OwnedObjects => m_ownedObjects;
    public string Name => m_agentName;

    public override void ProcessBehaviour()
    {
        Debug.Log("Moving towards target");
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
        m_navigationagent.SetDestination(target.transform.position);
        m_acting = Vector3.Distance(transform.position, target.transform.position) > 2f;
    }

    public void EnableDisable(OwnableObject target)
    {

    }

    public override bool ShouldEnter()
    {
        return m_acting;
    }

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override bool ShouldTransition()
    {
        throw new System.NotImplementedException();
    }
}
