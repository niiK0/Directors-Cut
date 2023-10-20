using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;
using Photon.Pun;

public class ChatManager : MonoBehaviour
{
    [SerializeField] GameObject chatObj;

    [SerializeField] GameObject messagesHolder;
    [SerializeField] GameObject messagePrefab;

    [SerializeField] TMP_InputField writeInput;

    PhotonView photonView;

    private bool chatOpen = false;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        chatObj.SetActive(chatOpen);
    }

    // Update is called once per frame
    void Update()
    {
        ToggleChat();
        CheckSendMessage();
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

            // Send the message using Photon PUN to all clients in the room
            photonView.RPC("ReceiveChatMessage", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, message);
            writeInput.text = string.Empty;
            EventSystem.current.SetSelectedGameObject(writeInput.gameObject);
            writeInput.OnPointerClick(new PointerEventData(EventSystem.current));
        }
        else
        {
            chatOpen = false;
            chatObj.SetActive(false);
        }
    }

    [PunRPC]
    private void ReceiveChatMessage(string sender, string message)
    {
        // Create a new message UI element and display it in the messagesHolder
        GameObject newMessage = Instantiate(messagePrefab, messagesHolder.transform);
        newMessage.GetComponent<ChatMessageScaler>().SetText(sender, message);
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
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (chatOpen)
            {
                chatOpen = false;
                chatObj.SetActive(false);
            }
        }
    }
}
