using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField] AgentBase[] m_agents;
    private AgentBase m_currentAgent;
    public string CurrentAgentName => m_currentAgent.GetType().Name;
    void Update()
    {
        m_currentAgent = EvaluateAgents();
        if(m_currentAgent)
            m_currentAgent.ProcessBehaviour();
    }

    private AgentBase EvaluateAgents()
    {
        for(int i = 0; i < m_agents.Length; i++)
        {
            if (m_agents[i].ShouldEnter())
            {
                return m_agents[i];
            }
        }
        return null;
    }
}
