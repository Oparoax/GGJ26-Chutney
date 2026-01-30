using UnityEngine;
using UnityEngine.AI;

public class HouseDweller : MonoBehaviour
{
    private Vector3 m_target;
    private NavMeshAgent m_agent;
    private GameObject m_player;
    private const float TESTOFFSET = 3f;


    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        var rac = FindFirstObjectByType<RacoonMove>();
        m_player = rac ? rac.transform.parent.gameObject : gameObject;
        m_target = GetPlayerPos() + Vector3.one.RandomVector(-TESTOFFSET, TESTOFFSET);
        m_target.y = 0;
        TellMeWereToGo(m_target);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x == m_target.x && transform.position.z == m_target.z)
        {
            m_target = GetPlayerPos() + Vector3.one.RandomVector(-TESTOFFSET, TESTOFFSET);
            m_target.y = -1;
            print(m_target);
            TellMeWereToGo(m_target);
        }
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

    private Vector3 GetPlayerPos()
    {
        return m_player.transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(m_target, Vector3.one * 0.5f);
    }
}