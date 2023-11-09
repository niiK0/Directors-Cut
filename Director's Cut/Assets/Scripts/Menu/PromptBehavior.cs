using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PromptBehavior : MonoBehaviourPunCallbacks
{
    public static PromptBehavior Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] Transform pwdPrompt;
    [SerializeField] TMP_Text pwdPromptRoomName;
    [SerializeField] TMP_InputField pwdInput;
    [SerializeField] Button pwdPromptJoinButton;

    private void TryPassword(string password, RoomInfo room)
    {
        string roomPwd = (string)room.CustomProperties["pwd"];
        if (string.Compare(password, roomPwd) == 0)
        {
            PhotonNetwork.JoinRoom(room.Name);
            pwdPrompt.gameObject.SetActive(false);
            Cameras.Instance.JoinLobbyCamera();
        }
        else
        {
            Debug.Log("Wrong Password");
        }
    }

    public void OpenPrompt(RoomInfo room)
    {
        pwdPrompt.gameObject.SetActive(true);
        pwdPromptRoomName.text = room.Name;
        pwdPromptJoinButton.onClick.AddListener(() => TryPassword(pwdInput.text, room));
    }
}
