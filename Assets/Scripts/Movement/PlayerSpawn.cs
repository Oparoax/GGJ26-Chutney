using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject player;
    void Start()
    {
        player.transform.position = transform.position;
    }
}
