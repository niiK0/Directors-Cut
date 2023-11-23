using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameFollowCam : MonoBehaviour
{
    void Update()
    {
        // Ensure the text always faces the main camera
        FaceToCamera();
    }

    void FaceToCamera()
    {
        // Find the main camera in the scene
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            // Calculate the direction from the text to the camera, only consider the horizontal rotation
            Vector3 lookDirection = mainCamera.transform.position - transform.position;
            lookDirection.y = 0f; // Set the y-component to zero to only consider horizontal rotation
            lookDirection = -lookDirection;

            // Ensure the text is always facing the camera horizontally
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
        else
        {
            Debug.LogWarning("Main camera not found in the scene. Make sure there is an active camera.");
        }
    }
}
