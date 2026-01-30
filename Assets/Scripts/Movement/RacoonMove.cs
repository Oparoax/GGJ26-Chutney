using System;
using UnityEngine;

public class RacoonMove : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private Transform _forward;
    [SerializeField] private float speed = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody>();
        }

        if (_forward == null)
        {
            Debug.LogError("Where's my transform forward bitch!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _rb.AddForce();
    }
}
