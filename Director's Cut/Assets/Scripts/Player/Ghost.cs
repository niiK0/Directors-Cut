using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] GameObject playerVisual;

    private PhotonView pv;

    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    public void SetGhostMode(bool isAlive)
    {
        playerVisual.SetActive(isAlive);
    }
}
