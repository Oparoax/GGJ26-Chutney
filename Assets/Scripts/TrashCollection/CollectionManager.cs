using System;
using UnityEngine;

public class CollectionManager : MonoBehaviour, IObservable
{
    [SerializeField] private float m_points;
    public float Points => m_points;
    void Start()
    {
        foreach (IGripTrash trash in FindObjectsByType<IGripTrash>(0))
            trash.Subscribe(this);
    }

    public void OnNotify<T>(RummageEvents racoonEvent, T data)
    {
        switch(racoonEvent)
        {
            case RummageEvents.collected:
                AddPoints((float)Convert.ChangeType(data, typeof(float)));
                break;
        }
    }

    private void AddPoints(float points)
    {
        m_points += points;
    }
}
