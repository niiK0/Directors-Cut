using UnityEngine;
using Photon.Pun;

public class Movement : MonoBehaviour
{
    //fields

    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;

    public float jumpForce = 5f;
    public float gravity = -9.81f; // Gravity value

    private float verticalRotation = 0f;
    private Vector3 playerVelocity;
    private CharacterController characterController;
    public Transform vCam;

    public Transform groundCheck;

    public bool isGrounded = false;
    public LayerMask groundLayer;

    public bool freezePlayer = false;
    public bool isInShortcut = false;

    PhotonView view;

    //events

    public event System.EventHandler<OnKeyPressedInShortcutModeEventArgs> OnKeyPressedInShortcutMode;
    public class OnKeyPressedInShortcutModeEventArgs : System.EventArgs
    {
        public KeyCode keyPress;
        public Movement playerMovement;
    }

    //

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        // Apply gravity
        ApplyGravity();

        if (view.IsMine && !freezePlayer)
        {
            // Player Movement
            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalMovement = Input.GetAxis("Vertical");
        
            if (Input.GetButton("Sprint"))
            {
                moveSpeed = 10f;
            }

            Vector3 moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

            moveSpeed = 5f;

            // Player Rotation
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

            transform.Rotate(Vector3.up * mouseX);
            vCam.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);


            // Player Jump
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                Jump();
            }
        }

        if (isInShortcut)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                OnKeyPressedInShortcutMode?.Invoke(this, new OnKeyPressedInShortcutModeEventArgs
                {
                    keyPress = KeyCode.A,
                    playerMovement = this
                });
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                OnKeyPressedInShortcutMode?.Invoke(this, new OnKeyPressedInShortcutModeEventArgs
                {
                    keyPress = KeyCode.D,
                    playerMovement = this
                });
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                OnKeyPressedInShortcutMode?.Invoke(this, new OnKeyPressedInShortcutModeEventArgs
                {
                    keyPress = KeyCode.F,
                    playerMovement = this
                });
            }
        }

    }

    private void ApplyGravity()
    {
        // Check if the player is grounded
        isGrounded = IsGrounded();

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Apply gravity
        playerVelocity.y += gravity * Time.deltaTime;

        // Move the player vertically
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
         Color gizmoColor = Color.green;

        Gizmos.color = gizmoColor;

        // Calculate the end position of the ray
        Vector3 endPosition = groundCheck.position + Vector3.down * .1f;

        // Draw the ray in the Scene view
        Gizmos.DrawLine(groundCheck.position, endPosition);
    }

    private bool IsGrounded()
    {
        float rayLength = .1f; // Adjust the length of the ray as needed.
        RaycastHit hit;

        // Cast a ray from the character's position downward.

        if (Physics.Raycast(groundCheck.position, Vector3.down, out hit, rayLength, groundLayer))
        {
            return true; // The character is grounded.
        }

        return false; // The character is not grounded.
    }

    private void Jump()
    {
        playerVelocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
    }

    public void EnableShortcutMode(bool par)
    {
        switch (par)
        {
            case true:
                isInShortcut = true;
                freezePlayer = true;
                break;
            case false:
                isInShortcut = false;
                freezePlayer = false;
                break;
        }
    }

    public void MovePlayer(Transform playerPosition)
    {
        transform.forward = playerPosition.forward;
        CharacterController playerController = GetComponent<CharacterController>();
        playerController.enabled = false;
        transform.position = playerPosition.transform.position;
        playerController.enabled = true;
    }
}
