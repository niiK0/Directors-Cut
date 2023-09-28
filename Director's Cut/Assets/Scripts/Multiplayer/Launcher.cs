using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    //Create Room
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_InputField roomPwdInputField;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] TMP_Text errorText;

    //Rooms List
    [SerializeField] GameObject roomTemplate;
    [SerializeField] GameObject roomHolder;

    [SerializeField] List<GameObject> roomsList;
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting to Server");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("title");
        Debug.Log("Joined Lobby");
    }

    private void RefreshRoomListCache(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (info.RemovedFromList)
            {
                cachedRoomList.Remove(info.Name);
            }
            else
            {
                cachedRoomList[info.Name] = info;
            }
        }
    }

    public void RefreshRoomList()
    {
        foreach (GameObject room in roomsList)
        {
            Destroy(room);
        }
        roomsList.Clear();

        foreach (KeyValuePair<string, RoomInfo> k in cachedRoomList)
        {
            print("Room: " + k.Value.Name + " ID: " + k.Value.masterClientId.ToString());
            GameObject room = Instantiate(roomTemplate);

            bool isPrivate = false;
            string roomPwd = (string)k.Value.CustomProperties["pwd"];
            if (!string.IsNullOrEmpty(roomPwd))
            {
                isPrivate = true;
            }

            room.GetComponent<RectTransform>().SetParent(roomHolder.transform, false);
            roomsList.Add(room);
            room.GetComponent<RoomTemplate>().AddRoom(k.Value.Name, k.Value.PlayerCount, k.Value.MaxPlayers, isPrivate, k.Value);
            room.SetActive(true);
        }
    }

    public void ListRooms()
    {
        MenuManager.Instance.OpenMenu("showrooms");

        RefreshRoomList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        RefreshRoomListCache(roomList);
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 6;

        if (!string.IsNullOrEmpty(roomPwdInputField.text))
        {
            //PWD TABLE
            ExitGames.Client.Photon.Hashtable table = new ExitGames.Client.Photon.Hashtable();
            table.Add("pwd", roomPwdInputField.text);
            options.CustomRoomProperties = table;
            options.CustomRoomPropertiesForLobby = new string[] {"pwd"};
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text, options);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Error creating room " + returnCode.ToString() + " : " + message;
        MenuManager.Instance.OpenMenu("error");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title");
    }
}
