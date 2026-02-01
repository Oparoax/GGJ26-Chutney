using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HuntingAgent : AgentBase
{
    private GameObject m_target;
    private const float SEARCHDIST = 5f;
    private const float KILLDIST = 0.5f;
    private bool m_playerCaught = false;
    public override void Enter()
    {
        
    }

    public override bool ShouldEnter()
    {
        GameObject tar = LocatePlayer();
        if(!tar)
            return false;
        if(!PlayerSearch(tar.transform.position, SEARCHDIST))
            return false;
        m_target = tar;
        return true;
    }

    public override bool ShouldTransition()
    {
        return CheckExit();
    }

    public override void ProcessBehaviour()
    {
        m_navigationagent.SetDestination(m_target.transform.position);
        OverlapTime();
    }

    private void OverlapTime()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position - (Vector3.up*0.5f), KILLDIST);
        foreach(Collider col in cols)
        {
            if(col.transform.root != null && col.transform.root.GetComponent<KillableEntity>() && !m_playerCaught)
            {
                StartCoroutine(col.transform.root.GetComponent<KillableEntity>().FuckingDie(1f));
                m_playerCaught = true;
            }
        }
    }

    private bool PlayerSearch(Vector3 playerPos, float dist)
    {
        return Vector3.Distance(transform.position, playerPos) < dist;
    }

    private GameObject LocatePlayer()
    {
        RacoonMove[] players = FindObjectsByType<RacoonMove>(FindObjectsSortMode.None);
        if (players != null && players.Length > 0)
            return players[0].gameObject;
        return null;
    }

    private bool CheckExit()
    {
        if (!m_target)
            return false;
        Debug.Log("Searching for KillDistance");
        Debug.Log($"{Vector3.Distance(transform.position, m_target.transform.position)}");
        if (m_playerCaught)
            return true;
        if (!PlayerSearch(m_target.transform.position, SEARCHDIST))
            return true;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position - (Vector3.up * 0.5f), KILLDIST);
    }
}
