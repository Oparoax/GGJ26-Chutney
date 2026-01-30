using UnityEngine;
using UnityEngine.InputSystem;

public class RacoonMove : MonoBehaviour
{
    private Rigidbody _rb;
    
    // Input actions
    private InputAction _move;
    private InputAction _grab;
    private InputAction _swipe;

    [SerializeField] private InputActionMap playerActions;
    
    [SerializeField] private Transform forward;
    [SerializeField] private float speed = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody>();
        }

        if (forward == null)
        {
            Debug.LogError("Where's my transform forward bitch!");
        }
        
        _move = playerActions.FindAction("Move");
        _grab = playerActions.FindAction("Grab");
        _swipe = playerActions.FindAction("Swipe");

        if (_move == null || _grab == null || _swipe == null)
        {
            Debug.LogError("Where the fuck are my input actions!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_move.triggered)
        {
            _rb.AddForce(_move.ReadValue<Vector2>().normalized * speed, ForceMode.Impulse);
        }
    }
}
