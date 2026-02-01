using UnityEngine;

public interface IObservable
{
    public void OnNotify<T>(RummageEvents racoonEvent, T data);
}
