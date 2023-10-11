using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] ItemInfo itemInfo;
    private GameObject player;

    public bool isEquipped { get; private set; } = false;
    public bool inInventory { get; private set; } = false;

    public int slotNumber = -1;

    private Rigidbody rb;

    [SerializeField] GameObject handler;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (this.isEquipped && Input.GetKeyDown(KeyCode.G))
        {
            Drop();
        }
    }

    //Interact
    public void Interact(GameObject playerObj)
    {
        Debug.Log("Interacted");
        player = playerObj;
        Pickup();
    }


    public void Pickup()
    {
        ItemManager inventory = player.GetComponent<ItemManager>();

        if ( inventory.GetItemsCount() < 3 )
        {
            inInventory = true;
            Debug.Log(itemInfo.itemName + " is now in inventory.");

            inventory.AddItem(gameObject);

            Equip();
        }
        else
        {
            Debug.Log("Inventory Full");
        }
    }

    public void Equip()
    {
        ItemManager itemManager = player.GetComponent<ItemManager>();
        int itemCount = itemManager.GetItemsCount();

        if (inInventory) 
        {
            for (int i = 0; i < itemCount; i++)
            {
                GameObject currentItemGameObject = itemManager.GetItemFromList(i);
                Item currentItem = currentItemGameObject.GetComponent<Item>();
                
                if (currentItem != null)
                {
                    if (currentItem.isEquipped)
                    {
                        currentItem.Unequip();

                        break;
                    } 
                }
            }

            gameObject.SetActive(true);

            gameObject.transform.position = handler.transform.position;
            transform.parent = handler.gameObject.transform;

            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            rb.detectCollisions = false;

            itemManager.currentSlot = slotNumber;
            isEquipped = true;
        }

        InventoryManager.Instance.UpdateUI();
    }


    public Sprite GetItemIcon()
    {
        return itemInfo.itemIcon;
    }

    public void Unequip()
    {
        isEquipped = false;
        ItemManager.Instance.currentSlot = -1;
        gameObject.SetActive(false);
        InventoryManager.Instance.UpdateUI();
    }

    public void Drop()
    {
        ItemManager itemManager = player.GetComponent<ItemManager>();

        itemManager.RemoveItem(gameObject);

        itemManager.currentSlot = -1;
        isEquipped = false;
        inInventory = false;

        rb.detectCollisions = true;
        transform.parent = null;
        rb.constraints = RigidbodyConstraints.None;

        Debug.Log(itemInfo.itemName + " has been dropped from inventory.");
        InventoryManager.Instance.UpdateUI();
    }
}
