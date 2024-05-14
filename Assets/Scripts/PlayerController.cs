using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of player movement
    public float gravity = 9.81f; // Strength of gravity

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 move = transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime;

        // Apply gravity
        if (!characterController.isGrounded)
        {
            move.y -= gravity * Time.deltaTime;
        }

        characterController.Move(move);
    }
}
