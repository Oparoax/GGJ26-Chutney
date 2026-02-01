using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillableEntity : MonoBehaviour, IKillable
{
    public IEnumerator FuckingDie(float delay)
    {
        Debug.Log("You should die");
        for (float i = 0; i < delay; i += Time.deltaTime)
            yield return null;
        SceneManager.LoadScene("GameOver");
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
