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

    [SerializeField] Transform pwdPrompt;
    [SerializeField] TMP_Text pwdPromptRoomName;
    [SerializeField] TMP_InputField pwdInput;
    [SerializeField] Button pwdPromptJoinButton;

    public void AddRoom(string roomName, int roomPlayers, int roomMaxPlayers, bool roomLocked, RoomInfo room)
    {
        roomNameText.text = roomName;
        roomPlayersText.text = roomPlayers.ToString() + "/" + roomMaxPlayers.ToString();
        roomLockedObj.enabled = roomLocked;
        roomInfo = room;
    }

    public void AttemptJoin()
    {
        string roomPwd = (string)roomInfo.CustomProperties["pwd"];
        if (string.IsNullOrEmpty(roomPwd))
        {
            PhotonNetwork.JoinRoom(roomInfo.Name);
        }
        else
        {
            OpenPrompt();
        }
    }

    private void TryPassword(string password)
    {
        string roomPwd = (string)roomInfo.CustomProperties["pwd"];
        if(string.Compare(password, roomPwd) == 0)
        {
            PhotonNetwork.JoinRoom(roomInfo.Name);
        }
        else
        {
            Debug.Log("Wrong Password");
        }
    }

    public void OpenPrompt()
    {
        pwdPrompt.gameObject.SetActive(true);
        pwdPromptRoomName.text = roomInfo.Name;
        pwdPromptJoinButton.onClick.AddListener(() => TryPassword(pwdInput.text));
    }
}
