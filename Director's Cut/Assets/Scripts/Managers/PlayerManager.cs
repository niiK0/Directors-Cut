using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    PhotonView PV;
    public bool isDirector { get; set; } = false;
    public bool isAlive { get; set; } = true;
    public int cachedActorNumber { get; set; }

    [SerializeField] private GameObject deadBodyPlayerPrefab;

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

    [PunRPC]
    public void KillPlayer(int viewId, Vector3 playerPos, Quaternion playerRot)
    {
        Instantiate(deadBodyPlayerPrefab, playerPos, playerRot);

        PhotonView playerView = PhotonView.Find(viewId);
        if (playerView == null) return;
        if (!playerView.IsMine) return;

        PhotonNetwork.Destroy(controller);
        isAlive = false;
        CreateGhost(playerPos, playerRot);
    }

    private void CreateGhost(Vector3 playerPos, Quaternion playerRot)
    {
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerGhost"), playerPos, playerRot, 0, new object[] { PV.ViewID });
        PV.RPC("HideDeadPlayer", RpcTarget.All, controller.GetPhotonView().ViewID);
    }

    [PunRPC]
    public void HideDeadPlayer(int viewId)
    {
        PhotonView playerView = PhotonView.Find(viewId);
        if (playerView == null) return;

        playerView.gameObject.GetComponent<Ghost>().SetGhostMode(this.isAlive);
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
