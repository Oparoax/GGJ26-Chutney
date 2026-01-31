using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


public delegate void Schedule(OwnableObject _target);

public class ScheduleManager : MonoBehaviour
{
    private const string FOLDERPATH = "/Resources/Schedule/";
    public static ScheduleManager x;
    private Dictionary<string, HashSet<OwnableObject>> m_ownedObjects = new Dictionary<string, HashSet<OwnableObject>>();
    private XMLSchedules m_scheduleXML;
    private ScheduleActionCollection m_actions;
    [SerializeField] private float m_currentTime;
    [SerializeField] private ScheduledAgent[] m_npcs;
    private Dictionary<string, List<ScheduledAgent>> m_agentMap = new Dictionary<string, List<ScheduledAgent>>();

    private void Start()
    {
        if (x == null)
            x = this;
        else
            Destroy(x);

        ReadAllActions();
        ReadSchedule();
        GenerateNPCsAndOwnedObjects();
        PairAgentsToObject();
    }

    public void Update()
    {
        m_currentTime = TimeManager.x.CurrentTimePercent;
        UpdateSchedules();
    }

    /// <summary>
    /// Parse all possible actions from the Actions XML
    /// </summary>
    private void ReadAllActions()
    {
        // Boilerplate XML reading.
        XmlSerializer actionSerializer = new XmlSerializer(typeof(ScheduleActionCollection));
        FileStream fs = new FileStream(Application.dataPath + FOLDERPATH + "Actions.xml", FileMode.Open);
        m_actions = actionSerializer.Deserialize(fs) as ScheduleActionCollection;
        fs.Close();
    }

    /// <summary>
    /// Read all schedules from the Schedule XML
    /// </summary>
    private void ReadSchedule()
    {
        XmlSerializer scheduleSerializer = new XmlSerializer(typeof(XMLSchedules));
        FileStream fs = new FileStream(Application.dataPath + FOLDERPATH + "Schedules.xml", FileMode.Open);
        m_scheduleXML = scheduleSerializer.Deserialize(fs) as XMLSchedules;
        fs.Close();
    }

    private void PairAgentsToObject()
    {
        foreach (XMLAgent agent in m_scheduleXML.m_agents)
        {
            m_agentMap.Add(agent.m_name, new List<ScheduledAgent>());
        }
        foreach (ScheduledAgent agent in m_npcs)
        {
            if (m_agentMap.ContainsKey(agent.Name))
                m_agentMap[agent.Name].Add(agent);
        }
    }

    private void GenerateNPCsAndOwnedObjects()
    {
        // Collect all possible Ownable Objects referenced
        for (int i = 0; i < m_npcs.Length; i++)
        {
            for (int j = 0; j < m_npcs[i].OwnedObjects.Length; j++)
            {
                OwnableObject objectToAdd = m_npcs[i].OwnedObjects[j].GetComponent<OwnableObject>();
                if (objectToAdd)
                {
                    if (!m_ownedObjects.ContainsKey(m_npcs[i].Name))
                        m_ownedObjects.Add(m_npcs[i].Name, new HashSet<OwnableObject>());
                    m_ownedObjects[m_npcs[i].Name].Add(objectToAdd);
                }
            }
        }
    }

    private void UpdateSchedules()
    {
        foreach (XMLAgent agent in m_scheduleXML.m_agents)
        {
            List<XMLEntry> validEntires = new List<XMLEntry>();
            foreach (XMLEntry entry in agent.m_entries)
            {
                bool timeSimil = m_currentTime >= entry.m_startTime;
                if (timeSimil)
                {
                    validEntires.Add(entry);
                }
            }
            if (validEntires.Count <= 0)
                continue;
            BuildSchedule(validEntires[validEntires.Count-1], agent.m_name);
        }
    }

    private ScheduleActions GetActionParams(string actionName)
    {
        foreach (ScheduleActions action in m_actions.m_actions)
        {
            if (action.m_name == actionName)
                return action;
        }
        return null;
    }

    private OwnableObject GetTarget(string ownername, string targetname)
    {
        foreach (OwnableObject target in m_ownedObjects[ownername])
            if (target.name == targetname)
                return target;
        return null;
    }

    private void BuildSchedule(XMLEntry entry, string ownerName)
    {
        XMLEntryAction entryaction = entry.m_actions[0];
        ScheduleActions action = GetActionParams(entryaction.m_name);
        OwnableObject target = GetTarget(ownerName, action.m_target);
        foreach (ScheduledAgent agent in m_agentMap[ownerName])
        {
            if (!agent.IsActing)
            {
                Debug.Log(entryaction.m_name);
                GetAppropriateSchedule(agent, action.m_type, target);
            }
        }

    }

    private void GetAppropriateSchedule(ScheduledAgent agent, ActionType functionType, OwnableObject target)
    {
        switch (functionType)
        {
            case ActionType.moveto:
                agent.UpdateSchedules((OwnableObject) => agent.MoveTo(target));
                break;
            case ActionType.animation:
                break;
        }
    }
}

