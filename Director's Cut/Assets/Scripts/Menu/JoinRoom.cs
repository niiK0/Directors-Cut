using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class JoinRoom : MonoBehaviour
{
    [SerializeField] Transform pwdPrompt;
    [SerializeField] TMP_Text pwdPromptRoomName;
    [SerializeField] TMP_InputField pwdInput;

    public void OpenPrompt(RoomInfo room)
    {
        pwdPrompt.gameObject.SetActive(true);
        pwdPromptRoomName.text = room.Name;
    }

    public void ClosePrompt()
    {
        pwdPrompt.gameObject.SetActive(false);
        pwdPromptRoomName.text = string.Empty;
        pwdInput.text = string.Empty;
    }
}
