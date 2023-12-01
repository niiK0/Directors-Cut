using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Shortcut : MonoBehaviour, IInteractable
{
    const int cameraChildIndex = 1;
    const int playerPositionChildIndex = 2;

    [SerializeField] private GameObject[] connectedShortcuts; //Indíce 0 deve ser a PlayerPosition do próprio Shortcut
    [SerializeField] private GameObject shortcutCamera;
    [SerializeField] private Transform secretRoom;
    private int currentShortcut;
    private GameObject currentCamera;

    public void Interact(GameObject player)
    {
        Debug.Log("Shortcut Interacted!");

        PlayerController playerMovement = player.GetComponent<PlayerController>();

        if(playerMovement.playerManager.isDirector)
        {
            Debug.Log("Director Entered Shortcut");
            //Muda a câmera atual para a do shortcut
            shortcutCamera.GetComponent<CinemachineVirtualCamera>().enabled = true;
            currentCamera = shortcutCamera;

            currentCamera.transform.rotation = gameObject.transform.rotation;

            //Coloca o jogador no "modo shortcut"
            playerMovement.EnableShortcutMode(true);

            //Envia o jogador para outra localização
            playerMovement.MovePlayer(secretRoom);

            //Subscreve ao evento do jogador
            playerMovement.OnKeyPressedInShortcutMode += OnKeyPress;

            //Define a porta selecionada como o currentShortcut
            currentShortcut = 0;

            PostProcessManager.Instance.SetVolume(1);
        }
    }

    private void ChangeToShortcutCamera(GameObject previousCamera, GameObject newCamera)
    {
        previousCamera.transform.rotation = Quaternion.identity;
        newCamera.transform.rotation = gameObject.transform.rotation;
        previousCamera.GetComponent<CinemachineVirtualCamera>().enabled = false;
        newCamera.GetComponent<CinemachineVirtualCamera>().enabled = true;
        currentCamera = newCamera;
    }

    private void OnKeyPress(object sender, PlayerController.OnKeyPressedInShortcutModeEventArgs e)
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
                e.playerMovement.MovePlayer(connectedShortcuts[currentShortcut].transform.GetChild(playerPositionChildIndex).transform);
                e.playerMovement.OnKeyPressedInShortcutMode -= OnKeyPress;
                e.playerMovement.transform.rotation = gameObject.transform.rotation;
                currentCamera.GetComponent<CinemachineVirtualCamera>().enabled = false;
                PostProcessManager.Instance.SetVolume(0);
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

