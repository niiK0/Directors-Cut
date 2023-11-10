using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    PhotonView PV;
    public bool isDirector { get; set; } = false;
    public bool isAlive { get; set; } = true;
    public int cachedActorNumber { get; set; }

    public GameObject controller;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        RoleManager.Instance.AddPlayer(this);
        cachedActorNumber = PV.Owner.ActorNumber;

        if (PV.IsMine)
        {
            CreateController();
        }
    }

    private void CreateController()
    {
        Debug.Log("MY ACTOR = " + cachedActorNumber);
        Transform spawnPoint = SpawnManager.Instance.GetSpawnPoint();
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnPoint.position, spawnPoint.rotation, 0, new object[] {PV.ViewID});
    }

    public void SyncPlayerTypeUI()
    {
        if (PV.IsMine)
        {
            RoleManager.Instance.SetPlayerTypeUI(isDirector);
            Debug.Log("checking if my player is director: " + isDirector);
        }
    }
}
