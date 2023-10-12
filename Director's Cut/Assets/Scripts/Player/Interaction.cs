using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

interface IInteractable
{
    public void Interact(GameObject playerObj);
}

public class Interaction : MonoBehaviour
{
    public float InteractRange;
    [SerializeField] CinemachineVirtualCamera cam;

    private void OnDrawGizmos()
    {
        Color gizmoColor = Color.green;

        Gizmos.color = gizmoColor;

        // Define the ray
        //Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, InteractRange));
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, InteractRange))
            Gizmos.DrawRay(ray);

        // Draw the ray using Gizmos
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            //Ray ray = new Ray(gameObject.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, InteractRange))
            {
                // Debug.Log("Hit object: " + hit.transform.name);
                if (hit.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact(gameObject);
                }
            }
        }
    }
}