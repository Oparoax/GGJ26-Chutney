using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    private List<IObservable> m_observers = new List<IObservable>();

    public void Subscribe(IObservable source)
    {
        m_observers.Add(source);
    }

    public void Unsubscribe(IObservable source)
    {
        m_observers.Remove(source);
    }

    public virtual void Notify<T>(RummageEvents racoonEvent, T data)
    {
        foreach(IObservable obs  in m_observers)
            obs.OnNotify(racoonEvent, data);
    }
}

public enum RummageEvents
{
    collected,
    dead
}