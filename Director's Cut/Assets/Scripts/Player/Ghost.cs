using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private bool isGhost = false;

    private MeshRenderer[] renderers;


    void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
    }

    public void SetGhostMode(bool enableGhost)
    {
        isGhost = enableGhost;

        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = !enableGhost;
        }


        if (isGhost)
        {
            // INFORM OTHER PLAYERS THAT THIS PLAYER IS NOW A GHOST (NETWORK)
        }
    }
}
