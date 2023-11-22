using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

public class Item : MonoBehaviourPunCallbacks, IInteractable
{
    [SerializeField] ItemInfo itemInfo;
    private GameObject player;

    public bool isEquipped { get; private set; } = false;
    public bool inInventory { get; private set; } = false;

    public string itemIdentifier;

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
        handler = playerObj.GetComponent<PlayerController>().handler;
        Pickup();
    }

    public void Pickup()
    {
        ItemManager inventory = ItemManager.Instance;

        if (inventory.GetItemsCount() < 3 )
        {
            inInventory = true;
            Debug.Log(itemInfo.itemName + " is now in inventory.");

            inventory.AddItem(gameObject);

            gameObject.transform.rotation = Quaternion.Euler(itemInfo.handRotation.x, itemInfo.handRotation.y, itemInfo.handRotation.z);

            Equip();
        }
        else
        {
            Debug.Log("Inventory Full");
        }
    }

    public void Equip()
    {
        ItemManager itemManager = ItemManager.Instance;
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

            EquipVisual();

            itemManager.currentSlot = slotNumber;
            isEquipped = true;

            player.GetComponent<PlayerController>().SyncItem(itemIdentifier);
        }

        InventoryManager.Instance.UpdateUI();
    }

    public void SetPlayer(GameObject playerObj)
    {
        player = playerObj;
        handler = playerObj.GetComponent<PlayerController>().handler;
    }

    public void EquipVisual()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);

        transform.parent = handler.gameObject.transform;
        gameObject.transform.localPosition = itemInfo.handPosition;
        gameObject.transform.localRotation = Quaternion.Euler(itemInfo.handRotation);

        //recheck rb for some reason lol
        rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        rb.detectCollisions = false;
    }

    public void UnequipVisual()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        transform.parent = handler.gameObject.transform;
        gameObject.transform.localPosition = itemInfo.handPosition;
        gameObject.transform.localRotation = Quaternion.Euler(itemInfo.handRotation);

        //recheck rb for some reason lol
        rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        rb.detectCollisions = false;
    }

    public Sprite GetItemIcon()
    {
        return itemInfo.itemIcon;
    }

    public Vector3 GetItemHandPosition()
    {
        return itemInfo.handPosition;
    }
    
    public Vector3 GetItemHandRotation()
    {
        return itemInfo.handRotation;
    }

    public void Unequip()
    {
        isEquipped = false;
        ItemManager.Instance.currentSlot = -1;
        UnequipVisual();
        InventoryManager.Instance.UpdateUI();
        player.GetComponent<PlayerController>().SyncItem(string.Empty);
    }

    public void Drop()
    {
        ItemManager itemManager = ItemManager.Instance;

        itemManager.RemoveItem(gameObject);

        itemManager.currentSlot = -1;
        isEquipped = false;
        inInventory = false;

        rb.detectCollisions = true;
        transform.parent = null;
        rb.constraints = RigidbodyConstraints.None;

        player.GetComponent<PlayerController>().SyncItem(string.Empty);

        Debug.Log(itemInfo.itemName + " has been dropped from inventory.");
        InventoryManager.Instance.UpdateUI();
    }
}
