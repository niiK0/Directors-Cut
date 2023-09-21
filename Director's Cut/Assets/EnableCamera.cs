using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class EnableCamera : MonoBehaviour
{
    public PhotonView view;
    public CinemachineVirtualCamera vcam;

    // Start is called before the first frame update
    void Start()
    {
        if (view.IsMine)
        {
            vcam.enabled = true;
        }
    }
}
