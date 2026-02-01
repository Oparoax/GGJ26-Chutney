using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HuntingAgent : AgentBase
{
    private GameObject m_target;
    private const float SEARCHDIST = 5f;
    private const float KILLDIST = 2f;

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
        if (PlayerSearch(m_target.transform.position, KILLDIST))
        {
            if (m_target.GetComponent<KillableEntity>())
            {
                m_target.GetComponent<KillableEntity>().FuckingDie(5f);
            }
            return true;
        }
        if (!PlayerSearch(m_target.transform.position, SEARCHDIST))
            return true;
        return false;
    }
}
