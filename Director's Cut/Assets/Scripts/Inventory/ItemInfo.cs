using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemInfo : ScriptableObject
{
    public GameObject itemGameObject;
    public string itemName;
    public Image itemIcon;

    public bool isEquipped = false;
    public bool inInventory = false;

    // Vectors for position/rotation in hand
    public Vector3 handPosition;
    public Vector3 handRotation;
}
