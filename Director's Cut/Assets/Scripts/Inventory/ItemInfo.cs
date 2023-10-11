using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemInfo : ScriptableObject
{
    public GameObject itemGameObject;
    public string itemName;
    public Sprite itemIcon;

    // Vectors for position/rotation in hand
    public Vector3 handPosition;
    public Vector3 handRotation;
}
