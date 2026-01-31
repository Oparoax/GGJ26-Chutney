using UnityEngine;

public class OwnableObject : MonoBehaviour
{

    [SerializeField] protected string m_id;
    public string ID { get { return m_id; } set { m_id = value; } }
}
