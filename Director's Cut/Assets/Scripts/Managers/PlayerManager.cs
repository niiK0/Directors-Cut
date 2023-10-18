using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
