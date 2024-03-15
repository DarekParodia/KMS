using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MultiInputSystem : MonoBehaviour
{   
    [SerializeField] private float _moveSpeed = 5.0f; 
    [SerializeField] public int LastDirection = 0;
    [SerializeField] private bool _isMoving = false;
    [SerializeField] private bool _isMovementEnabled = true;
    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!_isMovementEnabled) return;
        _rigidbody.velocity = _movementInput * _moveSpeed;
        LastDirection = GetDirection(_movementInput);
    }
    
    private void OnMove(InputValue value)
    {
        _movementInput = value.Get<Vector2>();
    }
    
    private int GetDirection(Vector2 movement)
    {
        // 0 = top, 1 = top right, 2 = right, 3 = bottom right, 4 = bottom, 5 = bottom left, 6 = left, 7 = top left
        if (movement != Vector2.zero) _isMoving = true;
        if (movement == Vector2.zero) _isMoving = false;
        if (movement == Vector2.zero) return LastDirection;
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        angle = (angle + 270) % 360; // Ensure angle is positive and 0 points to the top
        return (int)Mathf.Round(angle / 45) % 8;
    }
    
    public bool IsMoving()
    {
        return _isMoving;
    }
    
    public void enableMovement()
    {
        _isMovementEnabled = true;
    }
    public void disableMovement()
    {
        _isMovementEnabled = false;
    }
}
