using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Shortcut : MonoBehaviour, IInteractable
{
    const int cameraChildIndex = 1;
    const int playerPositionChildIndex = 2;

    [SerializeField] private GameObject[] connectedShortcuts; //Ind�ce 0 deve ser a PlayerPosition do pr�prio Shortcut
    [SerializeField] private GameObject shortcutCamera;
    [SerializeField] private Transform secretRoom;
    private int currentShortcut;
    private GameObject currentCamera;

    public void Interact(GameObject player)
    {
        Debug.Log("Shortcut Interacted!");

        Movement playerMovement = player.GetComponent<Movement>();

        //Muda a c�mera atual para a do shortcut
        shortcutCamera.GetComponent<CinemachineVirtualCamera>().enabled = true;
        currentCamera = shortcutCamera;

        //Coloca o jogador no "modo shortcut"
        playerMovement.EnableShortcutMode(true);

        //Envia o jogador para outra localiza��o
        playerMovement.MovePlayer(secretRoom);

        //Subscreve ao evento do jogador
        playerMovement.OnKeyPressedInShortcutMode += OnKeyPress;

        //Define a porta selecionada como o currentShortcut
        currentShortcut = 0;
    }

    private void ChangeToShortcutCamera(GameObject previousCamera, GameObject newCamera)
    {
        previousCamera.GetComponent<CinemachineVirtualCamera>().enabled = false;
        newCamera.GetComponent<CinemachineVirtualCamera>().enabled = true;
        currentCamera = newCamera;
    }

    private void OnKeyPress(object sender, Movement.OnKeyPressedInShortcutModeEventArgs e)
    {
        switch (e.keyPress)
        {
            case KeyCode.A:
                UpdateCurrentShortcut(-1);
                ChangeToShortcutCamera(currentCamera, connectedShortcuts[currentShortcut].transform.GetChild(cameraChildIndex).gameObject);
                break;
            case KeyCode.D:
                UpdateCurrentShortcut(1);
                ChangeToShortcutCamera(currentCamera, connectedShortcuts[currentShortcut].transform.GetChild(cameraChildIndex).gameObject);
                break;
            case KeyCode.F:
                e.playerMovement.EnableShortcutMode(false);
                e.playerMovement.MovePlayer(connectedShortcuts[currentShortcut].transform.GetChild(playerPositionChildIndex));
                e.playerMovement.OnKeyPressedInShortcutMode -= OnKeyPress;
                currentCamera.GetComponent<CinemachineVirtualCamera>().enabled = false;
                break;
        }
    }

    private void UpdateCurrentShortcut(int increment)
    {
        int newShortcutIndex = currentShortcut + increment;
        if (newShortcutIndex > connectedShortcuts.Length - 1)
        {
            newShortcutIndex = 0;
        }
        else if (newShortcutIndex < 0)
        {
            newShortcutIndex = connectedShortcuts.Length - 1;
        }

        currentShortcut = newShortcutIndex;
    }
}

