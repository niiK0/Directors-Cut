using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI text;

    public GameObject playerPrefab;

    public int playerNum = 0;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 randomPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0,1.5f,0), Quaternion.identity);
        playerNum = PhotonNetwork.CurrentRoom.PlayerCount;
        text.text = string.Format(text.text, PhotonNetwork.CurrentRoom.Name, playerNum);
    }

    public override void OnJoinedRoom()
    {
        print("player joined");
        playerNum++;
        text.text = string.Format(text.text, PhotonNetwork.CurrentRoom.Name, playerNum);
    }

    public override void OnLeftRoom()
    {
        print("player left");
        playerNum--;
        text.text = string.Format(text.text, PhotonNetwork.CurrentRoom.Name, playerNum);
    }
}
