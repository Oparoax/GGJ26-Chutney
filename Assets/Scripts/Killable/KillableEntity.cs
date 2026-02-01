using UnityEngine;
using UnityEngine.SceneManagement;

public class KillableEntity : MonoBehaviour, IKillable
{
    public void FuckingDie()
    {
        SceneManager.LoadScene("GameOver");
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
