using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _lookSpeedMouse = 2f;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private float _sprint = 1.5f;
    [SerializeField] private float _gravity = 9.8f;

    private Vector2 _rotation;
    private CharacterController _characterController;
    private float _velocity = 0f;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MouseLook();
        Move();
    }

    private void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * _lookSpeedMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _lookSpeedMouse * Time.deltaTime;

        _rotation.y += mouseX;
        _rotation.x -= mouseY;
        _rotation.x = Mathf.Clamp(_rotation.x, -90f, 90f);

        _camera.transform.eulerAngles = new Vector3(_rotation.x, _rotation.y, 0f);
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * Time.deltaTime;

        if (_characterController.isGrounded)
            _velocity = 0f;

        _velocity += Input.GetKeyDown(KeyCode.Space) ? Mathf.Sqrt(_jumpHeight * _gravity) : -_gravity * Time.deltaTime;

        vertical *= (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? _sprint : _moveSpeed;

        Vector3 moveDirection = _camera.transform.right * horizontal + _camera.transform.forward * vertical + Vector3.up * _velocity;
        _characterController.Move(moveDirection * Time.deltaTime);
    }
}
