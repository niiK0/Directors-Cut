using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{ 

    List<Item> inventory = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // se interagir com item
        // item.OnIteraction()

        if (gameObject.tag == "Item")
        {
            if (inventory.Count >= 3)
            {
                return;
            }

            if (inventory.Count < 3)
            {
                //inventory.Add(item);
            }
        }

    }
}
