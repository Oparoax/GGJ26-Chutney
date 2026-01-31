using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject m_target;
    private Vector3 m_offset;
    private Quaternion m_rotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_offset = transform.position;
        m_rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = m_rotation;
        transform.position = m_offset + m_target.transform.position;
    }
}
