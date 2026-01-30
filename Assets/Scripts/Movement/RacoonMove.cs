using UnityEngine;
using UnityEngine.InputSystem;

public class RacoonMove : MonoBehaviour
{
    // Input actions
    private InputAction _move;
    private InputAction _grab;
    private InputAction _swipe;
    
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject model;
    [SerializeField] private InputActionAsset playerActions;
    
    [SerializeField] private Transform forward;
    [SerializeField] private float speed = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if (forward == null)
        {
            Debug.LogError("Where's my transform forward bitch!");
        }
        
        _move = playerActions.FindAction("Player/Move");
        _grab = playerActions.FindAction("Player/Grab");
        _swipe = playerActions.FindAction("Player/Swipe");

        if (_move == null || _grab == null || _swipe == null)
        {
            Debug.LogError("Where the fuck are my input actions!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_move.IsPressed())
        {
            Vector2 inputValue = _move.ReadValue<Vector2>();
            Vector3 direction = new Vector3(inputValue.x, 0, inputValue.y);
            
            rb.AddForce(direction.normalized * speed, ForceMode.Impulse);
            
            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
            model.transform.rotation = Quaternion.Slerp(model.transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}
