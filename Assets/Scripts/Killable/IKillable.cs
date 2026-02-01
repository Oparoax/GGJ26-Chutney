using System.Collections;
using UnityEngine;

public interface IKillable
{
    public GameObject GetGameObject();
    public IEnumerator FuckingDie(float delay);
}
