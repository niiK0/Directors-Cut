using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string name;
    public ItemType type;
    public Image image;

    public void PickUp()
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();

        if (inventoryManager != null)
        {
            //inventoryManager.AddItem(this.gameObject);
            gameObject.SetActive(false);
        }
    }

    public void Shoot()
    {
        //Write logic for shooting
    }

    public void Use()
    {
        //Write logic for using
    }

    public Image GetImage()
    {
        return image;
    }

    public string GetName()
    {
        return name;
    }

}

public enum ItemType
{
    Weapon,
    Task
}
