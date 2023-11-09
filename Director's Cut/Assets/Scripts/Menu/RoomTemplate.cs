using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomTemplate : MonoBehaviour
{
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] TMP_Text roomPlayersText;
    [SerializeField] Image roomLockedObj;
    private RoomInfo roomInfo;

    public void AddRoom(string roomName, int roomPlayers, int roomMaxPlayers, bool roomLocked, RoomInfo room)
    {
        roomNameText.text = roomName;
        roomPlayersText.text = roomPlayers.ToString() + "/" + roomMaxPlayers.ToString();
        roomLockedObj.enabled = roomLocked;
        roomInfo = room;
    }

    public void SetupPrompt()
    {
        string roomPwd = (string)roomInfo.CustomProperties["pwd"];
        if (string.IsNullOrEmpty(roomPwd))
        {
            PhotonNetwork.JoinRoom(roomInfo.Name);
            Cameras.Instance.JoinLobbyCamera();
        }
        else
        {
            PromptBehavior.Instance.OpenPrompt(roomInfo);
        }
    }
}
