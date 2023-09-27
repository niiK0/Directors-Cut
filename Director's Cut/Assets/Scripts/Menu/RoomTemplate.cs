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
    public Button roomJoinButton;

    public void AddRoom(string roomName, int roomPlayers, int roomMaxPlayers, bool roomLocked)
    {
        roomNameText.text = roomName;
        roomPlayersText.text = roomPlayers.ToString() + "/" + roomMaxPlayers.ToString();
        roomLockedObj.enabled = roomLocked;
    }
}
