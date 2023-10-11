using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public ItemInfo itemInfo;
    public GameObject itemGameObject;

    public Rigidbody rb;

    public string promptMessage;

    public GameObject handler;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (this.itemInfo.isEquipped && Input.GetKeyDown(KeyCode.G))
        {
            Unequip();
        }
    }

    public void Interact(GameObject playerObj)
    {
        Debug.Log("Interacted");
        if (!this.itemInfo.inInventory)
        {
            
            this.gameObject.transform.position = handler.transform.position;
            this.transform.parent = handler.gameObject.transform;
            
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            Debug.Log(this.itemInfo.itemName + " is now a child of " + handler.gameObject.tag);
            rb.detectCollisions = false;

            this.itemInfo.inInventory = true;

            this.itemInfo.isEquipped = true;
        }
        else return;
    }

    public void Unequip()
    {
        rb.detectCollisions = true;
        this.transform.parent = null;
        Debug.Log("Item has been dropped");
        rb.constraints = RigidbodyConstraints.None;

        this.itemInfo.inInventory = false;

        this.itemInfo.isEquipped = false;
    }
}
