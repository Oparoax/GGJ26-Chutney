using UnityEngine;
using UnityEngine.AI;

public class HouseDweller : MonoBehaviour
{
    private Vector3 m_target;
    private NavMeshAgent m_agent;

    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        Vector3 targ = new Vector3().RandomVector(-10, 10);
        targ.y = -1;
        m_target = targ;
        print(targ);
        TellMeWereToGo(targ);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// Sets the passed in point to the Agent's next target position.
    /// </summary>
    /// <param name="_point">The next target Position.</param>
    public void TellMeWereToGo(Vector3 _point)
    {
        if (m_agent.SetDestination(_point)){
            print("Moving");
        }       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(m_target, Vector3.one * 0.5f);
    }
}