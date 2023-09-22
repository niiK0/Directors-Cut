using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

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

    public override void OnPlayerEnteredRoom(Player player)
    {
        print(player.UserId + " joined");
        playerNum++;
        text.text = "Connected to lobby {0}\nPlayers: {1}";
        text.text = string.Format(text.text, PhotonNetwork.CurrentRoom.Name, playerNum);
        print(playerNum);
    }

    public override void OnPlayerLeftRoom(Player player)
    {
        print(player.UserId + " left");
        playerNum--;
        text.text = "Connected to lobby {0}\nPlayers: {1}";
        text.text = string.Format(text.text, PhotonNetwork.CurrentRoom.Name, playerNum);
        print(playerNum);
    }
}
