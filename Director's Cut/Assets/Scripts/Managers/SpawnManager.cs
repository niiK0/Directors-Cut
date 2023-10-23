using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    public static SpawnManager Instance;
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();

    private void Awake()
    {
        Instance = this;
        GetSpawnPoints();
    }

    void GetSpawnPoints()
    {
        foreach (Transform child in transform)
        {
            spawnPoints.Add(child);
        }
    }

    public Transform GetSpawnPoint()
    {
        return spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber-1];
    }
}
