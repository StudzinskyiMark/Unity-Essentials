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

        Cursor.lockState = CursorLockMode.Locked;
    }

   

    private void Start()
    {
        _rotationX = transform.localEulerAngles.x;
        _rotationY = transform.localEulerAngles.y;

        if (_rotationX > 180)
        {
            _rotationX -= 360;
        }
    }

    void Update()
    {
        if (Application.isFocused)
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
            _rotationY += mouse.x;  
            _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

            transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        }
        
        
    }
    
    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();

        Cursor.lockState = CursorLockMode.None;
    }
}
