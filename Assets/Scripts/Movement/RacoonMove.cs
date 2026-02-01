using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RacoonMove : KillableEntity
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject grabBox;

    [SerializeField] private GameObject playerCamera;
    
    [SerializeField] private InputActionAsset playerActions;
    
    [SerializeField] private TrashCollector trashCollector;
    [SerializeField] private Swiper swiper;
    
    [SerializeField] private float walkSpeedMod = 0.75f;
    [SerializeField] private float runSpeedMod = 1f;
    
    [SerializeField] private float turnSpeed = 5f;
    
    [SerializeField] private float throwForce = 5f;
    [SerializeField] private Transform throwForceDirection;
    
    [SerializeField] private Animator animator;

    [SerializeField] private float _maxRunSpeed;
    [SerializeField] private float _maxWalkSpeed;
    
    [SerializeField] private float speed;
    
    // Input actions
    private InputAction _move;
    private InputAction _sprint;
    private InputAction _grab;
    private InputAction _swipe;
    private InputAction _throw;
    
    private string _moveAnimParam = "IsMoving";
    private string _sprintAnimParam = "IsSprinting";

    private bool _isRunning;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        
        // Map input action maps.
        _move = playerActions.FindAction("Player/Move");
        _sprint = playerActions.FindAction("Player/Sprint");
        _grab = playerActions.FindAction("Player/Grab");
        _swipe = playerActions.FindAction("Player/Swipe");
        _throw = playerActions.FindAction("Player/Throw");

        // Check all maps are valid.
        if (_move == null || _grab == null || _swipe == null || _sprint == null || _throw == null)
        {
            Debug.LogError("Where the fuck are my input actions!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_move.IsPressed())
        {
            animator.SetBool(_moveAnimParam, true);
            
            var speedMod = walkSpeedMod;
            var maxSpeed = _maxWalkSpeed;
            
            if (_sprint.IsPressed())
            {
                _isRunning = true;
                animator.SetBool(_sprintAnimParam, true);
                
                speedMod = runSpeedMod;
                maxSpeed = _maxRunSpeed;
            }
            else
            {
                _isRunning = false;
                animator.SetBool(_sprintAnimParam, false);
            }
            
            Vector2 inputValue = _move.ReadValue<Vector2>();
            Vector3 direction = new Vector3(inputValue.x , 0, inputValue.y);

            Vector3 forwardMov = direction.z * playerCamera.transform.forward;
            Vector3 rightMov = direction.x * playerCamera.transform.right;
            
            direction = forwardMov + rightMov;
            
            rb.AddForce(direction.normalized * speedMod, ForceMode.Impulse);
            
            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
            model.transform.rotation = Quaternion.Slerp(model.transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
            
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
            
            speed = rb.linearVelocity.magnitude;
        }
        else
        {
            animator.SetBool(_moveAnimParam, false);
        }

        if (!_isRunning)
        {
            if (_grab.WasPressedThisFrame())
            {
                Debug.Log("Grab");
                trashCollector.GrabToggle();
            }
        
            if (_swipe.WasPressedThisFrame())
            {
                Debug.Log("Swipe");
                swiper.Swipe();
            }

            if (_throw.WasPressedThisFrame())
            {
                Debug.Log("Throw");
                trashCollector.Throw(throwForceDirection.forward, throwForce);
            }
        }
        else
        {
            trashCollector.Drop();
        }
    }
}
