using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image iconImage;
    //public Text itemNameText;

    private GameObject item;

    // Initialize the slot with an item
    public void InitializeSlot(GameObject item)
    {
        this.item = item;
        UpdateSlotUI();
    }

    // Clear the slot
    public void ClearSlot()
    {
        item = null;
        UpdateSlotUI();
    }

    // Handle item interaction (e.g. use, drop)
    public void InteractWithSlot()
    {
        if (item != null)
        {
            // Implement item interactions here (e.g., use or drop the item)
            // You can access the item GameObject via 'item'
        }
    }

    // Update the slot's visual representation
    private void UpdateSlotUI()
    {
        if (item != null) 
        {
            iconImage = item.GetComponent<Item>().GetImage();
            //iconNameText= item.GetComponent<Item>().GetName();

        }
        else
        {
            iconImage.sprite = null;
            //itemNameText.text = "";
        }
    }
}
