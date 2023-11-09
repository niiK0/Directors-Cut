using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Death : MonoBehaviour, IInteractable
{
    public void Interact(GameObject player)
    {
        Debug.Log("Man I'm dead");
        RoleManager.Instance.FindPMByActorNumber(PhotonNetwork.LocalPlayer.ActorNumber).isAlive = false;
        Destroy(gameObject);
    }
}
