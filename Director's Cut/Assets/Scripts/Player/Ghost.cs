using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private MeshRenderer[] renderers;

    private PhotonView pv;

    void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
        pv = GetComponent<PhotonView>();
    }

    public void SetGhostMode()
    {
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }

    public void SetGhostModeRPC()
    {
        pv.RPC("SetGhostOfPlayer", RpcTarget.Others, pv.ViewID);
        SetGhostMode();
    }

    [PunRPC]
    private void SetGhostOfPlayer(int viewId)
    {
        GameObject targetPlayerObj = PhotonView.Find(viewId).gameObject;
        targetPlayerObj.GetComponent<Ghost>().SetGhostMode();
    }
}
