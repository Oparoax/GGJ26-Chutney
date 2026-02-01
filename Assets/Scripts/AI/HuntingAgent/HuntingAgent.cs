using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HuntingAgent : AgentBase
{
    private GameObject m_target;
    private const float SEARCHDIST = 5f;

    public override void Enter()
    {
        
    }

    public override bool ShouldEnter()
    {
        GameObject tar = LocatePlayer();
        if(!tar)
            return false;
        if(!PlayerNearby(tar.transform.position))
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
    }

    private bool PlayerNearby(Vector3 playerPos)
    {
        return Vector3.Distance(transform.position, playerPos) < SEARCHDIST;
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
        if (Vector3.Distance(m_target.transform.position, transform.position) <= 5f)
        {
            if (m_target.GetComponent<KillableEntity>())
            {
                m_target.GetComponent<KillableEntity>().FuckingDie(5f);
            }
            return true;
        }
        if (!PlayerNearby(m_target.transform.position))
            return true;
        return false;
    }
}
