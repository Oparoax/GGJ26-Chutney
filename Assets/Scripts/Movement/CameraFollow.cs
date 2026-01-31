using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject m_target;
    private Vector3 m_offset;
    private Quaternion m_rotation;
    
    void Start()
    {
        // Setup basic offsets based on camera position in scene
        m_offset = transform.position;
        m_rotation = transform.rotation;
    }

    void Update()
    {
        // Apply the basic offsets to the camera
        transform.rotation = m_rotation;
        transform.position = m_offset + m_target.transform.position;
    }
}
