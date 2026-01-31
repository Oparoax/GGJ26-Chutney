using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public enum ScheduleType
{
    none,
    simple,
    sequence,
    weightedrandom
}

[XmlRoot("Schedules")]
public class XMLSchedules
{
    [XmlElement("Agent")]
    public List<XMLAgent> m_agents;
}

[XmlRoot("Agent")]
public class XMLAgent
{
    [XmlAttribute("name")]
    public string m_name;
    [XmlElement("Entry")]
    public List<XMLEntry> m_entries;
}

[XmlRoot("Entry")]
public class XMLEntry
{
    [XmlAttribute("name")]
    public string m_name;
    [XmlAttribute("starttime")]
    public float m_startTime;
    [XmlAttribute("entrytype")]
    public ScheduleType m_scheduleType;
    [XmlElement("Action")]
    public List<XMLEntryAction> m_actions;
}

[XmlRoot("Action")]
public class XMLEntryAction
{
    [XmlAttribute("name")]
    public string m_name;
    [XmlAttribute("weight")]
    public float m_weight;
}