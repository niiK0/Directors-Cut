using UnityEngine;
using Photon.Pun;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;

    private float verticalRotation = 0f;
    private CharacterController characterController;

    PhotonView view;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (view.IsMine)
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
            Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }
}
