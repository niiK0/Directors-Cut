using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    //fields
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Vector3 moveDir;
    public float mouseSensitivity = 2f;

    [SerializeField] float jumpForce = 5f;
    [SerializeField] float gravity = -9.81f; // Gravity value

    private float verticalRotation = 0f;
    private Vector3 playerVelocity;
    private CharacterController characterController;
    [SerializeField] Transform vCam;

    [SerializeField] Transform groundCheck;

    [SerializeField] bool isGrounded = false;
    [SerializeField] LayerMask groundLayer;

    public bool freezePlayer = false;

    PhotonView view;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (!view.IsMine)
        {
            Destroy(GetComponentInChildren<CinemachineVirtualCamera>().gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (!view.IsMine)
            return;

        ApplyGravity();
        Move();
    }

    private void Update()
    {
        if (!view.IsMine)
            return;

        if (freezePlayer)
            return;


        GetMove();
        Rotate();
        Jump();
    }

    void Move()
    {
        characterController.Move(moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    void GetMove()
    {
        // Player Movement
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        moveDir = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }

    void Rotate()
    {
        // Player Rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);
        vCam.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
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
        characterController.Move(playerVelocity * Time.fixedDeltaTime);
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
        // Player Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }
}
