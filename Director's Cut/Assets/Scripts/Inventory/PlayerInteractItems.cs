using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractItems : MonoBehaviour, IInteractable
{

    public void Interact(GameObject playerObj)
    {
        Debug.Log(playerObj.name);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
