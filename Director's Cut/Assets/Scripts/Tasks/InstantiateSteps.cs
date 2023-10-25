using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateSteps : MonoBehaviour
{
    public void InstantiateCookingSteps()
    {
        
        string prefabFolderPath = "Items/";
        //ADicionar os items para cozinhar
        GameObject itemPrefab = Resources.Load<GameObject>(prefabFolderPath);

    }
}
