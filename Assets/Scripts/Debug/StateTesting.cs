using UnityEngine;
using TMPro;
public class StateTesting : MonoBehaviour
{
    [SerializeField] private TMP_Text m_label;
    [SerializeField] private GameObject m_target;
    [SerializeField] private AgentManager m_agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_label.text = m_agent.CurrentAgentName;
        m_label.transform.position = Camera.main.WorldToScreenPoint(m_target.transform.position);
    }


}
