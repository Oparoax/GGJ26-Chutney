using UnityEngine;
using UnityEngine.AI;

public abstract class AgentBase : MonoBehaviour
{
    protected NavMeshAgent m_navigationagent;

    public void Start()
    {
        m_navigationagent = GetComponent<NavMeshAgent>();
    }

    public abstract void ProcessBehaviour();
    public abstract bool ShouldEnter();
    public abstract void Enter();
    public abstract bool ShouldTransition();

}
