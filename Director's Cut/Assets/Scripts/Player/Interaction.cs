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
    private Transform highlight;
    [SerializeField] LayerMask layers;
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
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        //Ray ray = new Ray(gameObject.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        if (Physics.Raycast(ray, out hit, InteractRange, layers))
        {
            highlight = hit.transform;

            if (gameObject.CompareTag("GhostPlayer"))
            {
                if (!highlight.CompareTag("TaskItem"))
                {
                    highlight = null;
                    return;
                }
            }

            if (highlight.CompareTag("Item") || highlight.CompareTag("TaskItem"))
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = new Color32(222, 102, 107, 255);
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 5.0f;
                }
            }
            else
            {
                highlight = null;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                // Debug.Log("Hit object: " + hit.transform.name);
                if (hit.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    if(highlight)
                    {
                        if (!highlight.CompareTag("TaskItem") && gameObject.CompareTag("GhostPlayer"))
                        {
                            highlight = null;
                            return;
                        }

                        if(highlight.CompareTag("Item") || highlight.CompareTag("TaskItem"))
                            interactObj.Interact(gameObject);
                    }
                }
            }
        }
    }
}