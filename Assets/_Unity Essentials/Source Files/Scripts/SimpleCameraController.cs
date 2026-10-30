// using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleCameraController : MonoBehaviour
{
    public float moveSpeed = 3.0f;           // Movement speed
         
    public float mouseSensitivity = 1.0f;    // Mouse look sensitivity

    public InputAction moveAction;
    public InputAction lookAction;

    private float _rotationX = 0.0f;          // Rotation around the X axis (up and down look)
    private float _rotationY = 0.0f;          // Rotation around the Y axis (left and right look)

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();

        // CursorLockMode.
    }

    private void Start()
    {
        _rotationX = transform.localEulerAngles.y;
        _rotationY = transform.localEulerAngles.x;

        if (_rotationX > 180)
        {
            _rotationX -= 360;
        }
    }

    void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        
        // Movement
        float moveForward = moveInput.y * moveSpeed * Time.deltaTime;
        Vector3 moveDirection = new Vector3(transform.forward.x, 0.0f, transform.forward.z).normalized;
        transform.Translate(moveDirection * moveForward, Space.World);
        
        float moveSide = moveInput.x * moveSpeed * Time.deltaTime;
        Vector3 sideDirection = new Vector3(transform.right.x, 0.0f, transform.right.z).normalized;
        transform.Translate(sideDirection * moveSide, Space.World);
        
        // Mouse Look
        Vector2 mouse = lookAction.ReadValue<Vector2>() * mouseSensitivity;

        _rotationX -= mouse.y;  
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
        
        _rotationY += mouse.x;  
        // rotationY = Mathf.Clamp(rotationY, -180f, 180f);  

        transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
    }
    
    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();

        // CursorLockMode.Disable();
    }
}
