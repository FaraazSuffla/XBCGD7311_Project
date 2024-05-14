using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public float sensitivity = 2f; // Mouse sensitivity

    private float rotationX = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor
    }

    void Update()
    {
        // Camera rotation
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limit vertical rotation

        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f); // Rotate camera vertically
        player.Rotate(Vector3.up * mouseX); // Rotate player horizontally
    }
}
