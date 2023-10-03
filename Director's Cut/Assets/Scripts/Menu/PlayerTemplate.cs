using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTemplate : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text playerNameText;
    [SerializeField] Button kickBtn;
    [SerializeField] Button giveHostBtn;

    [SerializeField] Player player;

    public void AddPlayer(Player _player)
    {
        playerNameText.text = _player.NickName;
        player = _player;
        if(PhotonNetwork.LocalPlayer != player)
        {
            kickBtn.gameObject.SetActive(PhotonNetwork.IsMasterClient);
            giveHostBtn.gameObject.SetActive(PhotonNetwork.IsMasterClient);
        }
    }

    public Player GetPlayer()
    {
        return player;
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Transform parent = transform.parent;
        foreach (Transform child in parent)
        {
            Player _cPlayer = child.GetComponent<PlayerTemplate>().GetPlayer();
            if (PhotonNetwork.LocalPlayer != _cPlayer)
            {
                child.GetChild(1).gameObject.SetActive(PhotonNetwork.IsMasterClient);
                child.GetChild(2).gameObject.SetActive(PhotonNetwork.IsMasterClient);
            }
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }

    public void KickPlayer()
    {
        PhotonNetwork.CloseConnection(player);
    }

    public void GiveHost()
    {
        PhotonNetwork.CurrentRoom.SetMasterClient(player);
    }
}
