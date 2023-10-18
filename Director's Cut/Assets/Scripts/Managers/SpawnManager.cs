using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    public static SpawnManager Instance;
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();
    private const string UsedSpawnPointProperty = "UsedSpawnPoint";

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

    public void SpawnPlayer(Transform spawnPoint)
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogError("No available spawn points left!");
            return;
        }

        if (spawnPoints.Contains(spawnPoint))
        {
            // Set a room property to mark the spawn point as used
            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { UsedSpawnPointProperty, spawnPoints.IndexOf(spawnPoint) } });
        }
    }

    public Transform GetSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey(UsedSpawnPointProperty))
        {
            // Adjust the available spawn points list based on the updated property.
            int usedSpawnIndex = (int)propertiesThatChanged[UsedSpawnPointProperty];
            spawnPoints.RemoveAt(usedSpawnIndex);
        }
    }
}
