using ExitGames.Client.Photon.StructWrapping;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    [SerializeField] GameObject handler;
    private List<GameObject> itemsInInventory = new List<GameObject>();
    bool[] slots = {false, false, false};
    public int currentSlot = -1;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            foreach(GameObject item in itemsInInventory)
            {
                Debug.Log(item.name);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) Manager(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) Manager(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Manager(2);
    }


    public void AddItem(GameObject itemToAdd)
    {
        if (itemToAdd)
        {
            itemsInInventory.Add(itemToAdd);
            for (int i = 0; i < slots.Length; i++)
            {
                if (!slots[i])
                {
                    slots[i] = true;
                    itemToAdd.GetComponent<Item>().slotNumber = i;
                    break;
                }
            }
        }
    }


    public void RemoveItem(GameObject itemToRemove)
    {
        if (itemsInInventory.Contains(itemToRemove))
        {
            itemsInInventory.Remove(itemToRemove);
            
            slots[itemToRemove.GetComponent<Item>().slotNumber] = false;
            itemToRemove.GetComponent<Item>().slotNumber = -1;
        }
    }


    public int GetItemsCount()
    {
        return itemsInInventory.Count;
    }


    public GameObject GetItemFromList(int index)
    {
        return itemsInInventory[index];
    }

    public GameObject GetItemBySlot(int slotNumber)
    {
        foreach (GameObject itemInInv in itemsInInventory)
        {
            if (itemInInv.GetComponent<Item>().slotNumber == slotNumber)
                return itemInInv;
        }
        return null;
    }

    public void Manager(int index)
    {
        if (slots[index] == false) return;

        GameObject itemGameObject = null;
        
        foreach (GameObject itemInInv in itemsInInventory)
        {
            if (itemInInv.GetComponent<Item>().slotNumber == index)
                itemGameObject = itemInInv;
        }

        if (itemGameObject == null) return;
       
        Item item = itemGameObject.GetComponent<Item>();

        if (item == null) return;

        if (item.isEquipped)
        {
            item.Unequip();
        }
        else
        {
            item.Equip();
        }
    }
}
