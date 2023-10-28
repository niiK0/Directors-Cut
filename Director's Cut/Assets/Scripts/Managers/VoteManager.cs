using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class VoteManager : MonoBehaviour
{
    public static VoteManager Instance;

    [SerializeField] private GameObject VMCanvas;
    [SerializeField] private Button[] playerIcons;

    private void Awake()
    {
        Instance = this;
    }

    private void ShowVote()
    {
        VMCanvas.SetActive(true);

        //remover depois, está aqui só para debug
        RoleManager.Instance.SetUpRoles();

        SetButtons();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void SetButtons()
    {
        List<PlayerManager> playerList = RoleManager.Instance.GetPlayerList();
        int nOfPlayers = playerList.Count;

        for (int i = 0; i < nOfPlayers; i++)
        {
            playerIcons[i].gameObject.SetActive(true);
            if (playerList[i].PV.IsMine)
            {
            playerIcons[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = PhotonNetwork.LocalPlayer.NickName;
            }  
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ShowVote();
        }
    }
}
