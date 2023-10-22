using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    public bool IsDirector { get; set; } = false;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    private void CreateController()
    {
        Transform spawnPoint = SpawnManager.Instance.GetSpawnPoint();
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnPoint.position, spawnPoint.rotation);
        SpawnManager.Instance.SpawnPlayer(spawnPoint);
    }

    //Função de Debug criada para testar as funcionalidades do RoleManager
    private void DebuggingRoles()
    {

        if (PV.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                RoleManager.Instance.SetUpRoles();
                foreach (PlayerManager playerManager in RoleManager.Instance.GetPlayerList())
                {
                    Debug.Log(playerManager.IsDirector);
                }
            }
        }
       
    }

    private void Update()
    {
        //DebuggingRoles();
    }
}
