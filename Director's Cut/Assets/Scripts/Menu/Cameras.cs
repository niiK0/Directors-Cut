using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Cameras : MonoBehaviour
{
    public static Cameras Instance;

    [SerializeField] CinemachineVirtualCamera RoomlistToLobby;
    [SerializeField] CinemachineVirtualCamera LobbyToRoomlist;
    [SerializeField] CinemachineVirtualCamera LobbyCam;
    [SerializeField] CinemachineVirtualCamera RoomListCam;
    [SerializeField] Menu LobbyMenu;
    [SerializeField] Menu RoomlistMenu;

    [SerializeField] CinemachineVirtualCamera currentMovingCamera;
    [SerializeField] CinemachineVirtualCamera currentTargetCamera;
    [SerializeField] CinemachineVirtualCamera currentCamera;
    [SerializeField] CinemachineDollyCart currentDolly;
    [SerializeField] Volume postProcess;
    private Bloom bloom;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDolly == null)
            return;

        if (currentDolly.m_Path.PathLength <= currentDolly.m_Position)
        {
            currentMovingCamera.enabled = false;
            currentTargetCamera.enabled = true;
            currentDolly = null;
        }
    }

    public void JoinLobbyCamera()
    {
        SetTargetCam(LobbyCam);
        SetCurrentCam(RoomListCam);
        MoveCamera(RoomlistToLobby);
        MenuManager.Instance.CloseMenu(RoomlistMenu);
    }

    public void GetKicked()
    {
        SetTargetCam(RoomListCam);
        SetCurrentCam(LobbyCam);
        MoveCamera(LobbyToRoomlist);
        MenuManager.Instance.CloseMenu(LobbyMenu);
    }

    public void MoveCamera(CinemachineVirtualCamera camToMove)
    {
        currentMovingCamera = camToMove;
        currentDolly = currentMovingCamera.GetComponent<CinemachineDollyCart>();
        currentDolly.m_Position = 0;
        currentMovingCamera.enabled = true;
        currentDolly.enabled = true;
        currentCamera.enabled = false;
    }

    public void SetTargetCam(CinemachineVirtualCamera targetCam)
    {
        currentTargetCamera = targetCam;
    }

    public void SetCurrentCam(CinemachineVirtualCamera targetCam)
    {
        currentCamera = targetCam;
    }

    public void SetBloom(float quantity)
    {
        postProcess.profile.TryGet(out bloom);
        bloom.threshold.value = quantity;
    }
}
