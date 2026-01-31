using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

[System.Serializable]
public class ScheduleActions
{
    [XmlAttribute("name")]
    public string m_name;
    [XmlAttribute("target")]
    public string m_target;
    [XmlAttribute("type")]
    public ActionType m_type;
}

[System.Serializable, XmlRoot(ElementName ="Actions")]
public class ScheduleActionCollection
{
    [XmlElement("Action")]
    public List<ScheduleActions> m_actions = new List<ScheduleActions>();
}

public enum ActionType
{
    moveto,
    enabledisable,
    animation
}
