using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cameras : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera currentMovingCamera;
    [SerializeField] CinemachineVirtualCamera currentTargetCamera;
    [SerializeField] CinemachineDollyCart currentDolly;
    [SerializeField] Menu currentTargetMenu;

    // Update is called once per frame
    void Update()
    {
        if (currentDolly == null)
            return;

        if (currentDolly.m_Path.PathLength <= currentDolly.m_Position)
        {
            currentMovingCamera.enabled = false;
            currentTargetCamera.enabled = true;
            //MenuManager.Instance.OpenMenu(currentTargetMenu);
            currentDolly = null;
        }
    }

    public void MoveCamera(CinemachineVirtualCamera camToMove)
    {
        currentMovingCamera = camToMove;
        currentDolly = currentMovingCamera.GetComponent<CinemachineDollyCart>();
        currentDolly.m_Position = 0;
        currentMovingCamera.enabled = true;
        currentDolly.enabled = true;
    }

    public void SetTargetCam(CinemachineVirtualCamera targetCam)
    {
        currentTargetCamera = targetCam;
    }

    public void SetTargetMenu(Menu menu)
    {
        currentTargetMenu = menu;
    }
}
