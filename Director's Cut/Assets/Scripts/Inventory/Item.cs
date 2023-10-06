using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour, IInteractable
{
    public string name;
    public Image image;

    public Vector3 handPosition;
    public Vector3 handRotatiom;


    public void Interact(GameObject player)
    {
        PickUp();
    }

    public void PickUp()
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();

        if (inventoryManager != null)
        {
            //inventoryManager.AddItem(this.gameObject);
            gameObject.SetActive(false);
        }
    }
    
    public void Use()
    {
        //Write logic for using
    }
    
    public string GetName()
    {
        return name;
    }
    
    public Image GetImage()
    {
        return image;
    }
}


