using UnityEngine;

public class SpiderController : MonoBehaviour
{
    public Transform player;  // Reference to the player GameObject
    public float moveSpeed = 3f;  // Adjust the speed as needed
    public float gravity = 9.8f;  // Adjust the gravity strength as needed
    private CharacterController characterController;

    private float verticalVelocity = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate the direction from the spider to the player
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0f;  // Ignore vertical movement

            // Rotate towards the player
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToPlayer), 0.1f);

            // Check if the spider is grounded
            if (characterController.isGrounded)
            {
                verticalVelocity = -gravity * Time.deltaTime;  // Reset vertical velocity when grounded
            }

            Debug.Log($"Is Grounded: {characterController.isGrounded}");

            // Apply gravity
            ApplyGravity();

            // Move towards the player
            Vector3 moveDirection = directionToPlayer.normalized;
            moveDirection.y = player.position.y;  // Include vertical movement
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            // Apply gravity manually
            verticalVelocity -= gravity * Time.deltaTime;
        }
    }
}
