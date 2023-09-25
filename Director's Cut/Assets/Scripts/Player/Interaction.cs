using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}

public class Interaction : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        Color gizmoColor = Color.green;

        Gizmos.color = gizmoColor;

        // Define the ray
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, InteractRange));

        // Draw the ray using Gizmos
        Gizmos.DrawRay(ray);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, InteractRange));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Debug.Log("Hit object: " + hit.transform.name);
                if (hit.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }

            //Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            //if(Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            //{
            //    if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            //    {
            //        interactObj.Interact();
            //    }
            //}
        }
    }
}
