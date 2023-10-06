using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public List<GameObject> slots = new List<GameObject>();

    public void AddItem(GameObject item)
    {
        if(slots.Count < 3)
        {
            slots.Add(item);
            UpdateUI();
        }
        else
        {
            Debug.Log("Inventory is Full");
        }
    }

    public void RemoveItem(GameObject item)
    {
        if(slots.Contains(item))
        {
            slots.Remove(item);
        }

        UpdateUI();
    }

    void UpdateUI()
    {


        // Update your UI elements to display the current inventory contents
        // For example, you can update the slot icons and labels here.
    }
}
