using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;
using Photon.Pun;
using Photon.Chat;
using ExitGames.Client.Photon;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    public static ChatManager Instance;

    ChatClient chatClient;

    [SerializeField] GameObject chatObj;

    [SerializeField] TMP_Text messagesHolder;

    [SerializeField] TMP_InputField writeInput;

    private bool chatOpen = false;
    private bool wasOpen = false;

    private bool isConnected = false;

    private string roomChat;

    private PlayerController playerObj;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Doing connection to chat..");
            chatObj.SetActive(chatOpen);
            chatClient = new ChatClient(this);
            chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(PhotonNetwork.LocalPlayer.NickName));
            roomChat = "Room" + PhotonNetwork.CurrentRoom.Name;
            isConnected = true;
            Debug.Log("Chat room name defined to " + roomChat);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isConnected)
        {
            chatClient.Service();
            ToggleChat();
            CheckSendMessage();
        }

    }

    public void SetPlayerObj(PlayerController player)
    {
        playerObj = player;
    }

    private void CheckSendMessage()
    {
        if (chatOpen && Input.GetKeyDown(KeyCode.Return))
        {
            SendChatMessage();
        }
    }

    private void SendChatMessage()
    {
        if (!string.IsNullOrEmpty(writeInput.text))
        {
            string message = writeInput.text;

            writeInput.text = string.Empty;
            EventSystem.current.SetSelectedGameObject(writeInput.gameObject);
            writeInput.OnPointerClick(new PointerEventData(EventSystem.current));
            chatClient.PublishMessage(roomChat, message);
        }
        else
        {
            chatOpen = false;
            chatObj.SetActive(false);
            playerObj.freezePlayer = false;
        }
    }

    private void ToggleChat()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            if (!chatOpen)
            {
                chatOpen = true;
                chatObj.SetActive(true);
                EventSystem.current.SetSelectedGameObject(writeInput.gameObject);
                writeInput.OnPointerClick(new PointerEventData(EventSystem.current));
                playerObj.freezePlayer = true;
            }
            else if(!wasOpen && chatOpen)
            {
                StopAllCoroutines();
                EventSystem.current.SetSelectedGameObject(writeInput.gameObject);
                writeInput.OnPointerClick(new PointerEventData(EventSystem.current));
                playerObj.freezePlayer = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (chatOpen)
            {
                chatOpen = false;
                chatObj.SetActive(false);
                playerObj.freezePlayer = false;
            }
        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log("CHAT DEBUG: LVL " + level + " - " + message);
    }

    public void OnDisconnected()
    {
        isConnected = false;
        Debug.Log("Disconnected from chat server");
    }

    public void OnConnected()
    {
        isConnected = true;
        chatClient.Subscribe(new string[] { roomChat.ToString() });
        Debug.Log("Connected to chat server");
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log("Chat state changed to " + state);
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        string msgs = "";

        bool wasOpen = chatOpen;

        if(!chatOpen)
        {
            chatOpen = true;
            chatObj.SetActive(true);
        }

        for (int i = 0; i < senders.Length; i++)
        {
            msgs = string.Format("<color=green>{0}</color><color=white>: {1}</color>", senders[i], messages[i]);

            messagesHolder.text += "\n "+ msgs;

            Debug.Log(msgs);
        }

        if (!wasOpen)
        {
            StartCoroutine(closeChat());
        }
    }

    IEnumerator closeChat()
    {
        yield return new WaitForSecondsRealtime(2);
        chatOpen = false;
        chatObj.SetActive(false);
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        for (int i = 0; i < channels.Length; i++)
        {
            Debug.Log("Subscribed to " + channels[i] + " " + results[i]);
        }
    }

    public void OnUnsubscribed(string[] channels)
    {
        for (int i = 0; i < channels.Length; i++)
        {
            Debug.Log("Unsubscribed to " + channels[i]);
        }
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        Debug.Log("chat status changed for " + user + " : " + status + " | " + gotMessage + " | " + message);
    }

    public void OnUserSubscribed(string channel, string user)
    {
        Debug.Log(user + " subscribed to " + channel);
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        Debug.Log(user + " unsubscribed to " + channel);
    }
}
