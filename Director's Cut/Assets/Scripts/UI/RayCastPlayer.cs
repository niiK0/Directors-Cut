using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCastPlayer : MonoBehaviour
{
    private bool isHovered = false;
    private GameObject hitGameObject = null;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object has a collider (you might want to refine this check based on your scene)
            if (hit.collider.CompareTag("UIInteract"))
            {
                // Object entered
                if (!isHovered)
                {
                    Debug.Log("Entered: " + hit.collider.gameObject.name);
                    hitGameObject = hit.collider.gameObject;
                    isHovered = true;
                    hitGameObject.GetComponent<Image>().color = hitGameObject.GetComponent<Button>().colors.highlightedColor;
                    // Handle enter logic here
                }

                // Object clicked
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Clicked: " + hit.collider.gameObject.name);
                    hitGameObject.GetComponent<Button>().onClick.Invoke();
                    // Handle click logic here
                }
            }
            else
            {
                if(isHovered && hitGameObject != null)
                {
                    Debug.Log("Exited");
                    hitGameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    isHovered = false;
                    hitGameObject = null;
                }
            }
        }
    }
}
