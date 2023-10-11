using ExitGames.Client.Photon.StructWrapping;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private ItemInfo itemInfo;
    private GameObject item;
    public GameObject handler;
    public List<GameObject> itemsInInventory = new List<GameObject>();

    private void Update()
    {
        foreach ( Transform child in handler.transform)
        {
            if(child.tag == "Item")
            {
                itemsInInventory.Add(child.gameObject);
            }
        }

        foreach(GameObject item in itemsInInventory)
        {
            itemInfo = item.gameObject.transform.GetComponent<Item>().itemInfo;
            if (item == null)
                return;

            if(item != null)
                Debug.Log(itemInfo.itemName + " is in inventory");

            if(!itemInfo.inInventory)
            {
                itemsInInventory.Remove(item);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) Manager(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) Manager(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Manager(2);
    }

    public void Manager(int index)
    {
        if (itemsInInventory == null) return;
        else
        {
            item = itemsInInventory.ElementAt(index);
            itemInfo = itemsInInventory.ElementAt(index).transform.GetComponent<ItemInfo>();
            if (itemInfo.inInventory && itemInfo.isEquipped)
            {
                itemInfo.isEquipped = false;
                item.SetActive(false);
            }
            else if(itemInfo.inInventory && !itemInfo.isEquipped)
            {
                itemInfo.isEquipped = true;
                item.SetActive(true);
            }
        }
    }
}
