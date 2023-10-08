using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseReporter : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform meetingPlace;
    [SerializeField] private WarningScript warning;

    public void Interact(GameObject playerObj)
    {
        Debug.Log("Interacted with button");
        StartCoroutine(warning.CallWarning());
        PlayerController playerMovement = playerObj.GetComponent<PlayerController>();
        playerMovement.MovePlayer(meetingPlace);
        playerMovement.freezePlayer = true;
        ReporterFunction();
    }

    protected abstract void ReporterFunction();
}
