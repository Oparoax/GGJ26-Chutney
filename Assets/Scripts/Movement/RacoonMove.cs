using UnityEngine;
using UnityEngine.InputSystem;

public class RacoonMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject grabBox;
    
    [SerializeField] private Animator anim;
    
    [SerializeField] private InputActionAsset playerActions;
    
    [SerializeField] private float speed = 1f;
    
    // Input actions
    private InputAction _move;
    private InputAction _grab;
    private InputAction _swipe;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
        
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        
        grabBox.SetActive(false);
        
        // Map input action maps.
        _move = playerActions.FindAction("Player/Move");
        _grab = playerActions.FindAction("Player/Grab");
        _swipe = playerActions.FindAction("Player/Swipe");

        // Check all maps are valid.
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

        if (_grab.WasPressedThisFrame())
        {
            Debug.Log("Grab");
            
            anim.SetTrigger("Grab");
        }
        
        if (_swipe.WasPressedThisFrame())
        {
            Debug.Log("Swipe");
        }
    }
}
