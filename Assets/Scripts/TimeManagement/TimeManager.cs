using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager x;
    private float m_currentTime;
    private const float m_dayLength = 3000f;
    public float CurrentTimePercent => m_currentTime / m_dayLength;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (x == null)
            x = this;
        else
            Destroy(x);
    }

    // Update is called once per frame
    void Update()
    {
        m_currentTime += Time.deltaTime;
        if (m_currentTime >= m_dayLength)
            m_currentTime = 0f;
    }
}
