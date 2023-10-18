using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseReporter : MonoBehaviourPunCallbacks, IInteractable
{
    public Transform meetingPlace;
    [SerializeField] WarningScript warning;

    public void Interact(GameObject playerObj)
    {
        Debug.Log("Interacted with btn");
        StartCoroutine(warning.CallWarning());
        PlayerController playerMovement = playerObj.GetComponent<PlayerController>();
        playerMovement.MovePlayer(meetingPlace);
        playerMovement.freezePlayer = true;
        ReporterFunction(playerObj);
    }

    protected abstract void ReporterFunction(GameObject playerObj);
}
