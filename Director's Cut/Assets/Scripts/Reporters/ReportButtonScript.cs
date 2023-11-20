using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class ReportButtonScript : BaseReporter
{
    VoteManager voteManager;
    [SerializeField] List<Transform> meetingPoints = new List<Transform>();

    private void Start()
    {
        voteManager = VoteManager.Instance;
    }

    protected override void ReporterFunction(GameObject playerObj)
    {
        photonView.RPC("SeatPlayerRPC", RpcTarget.All);
        //SeatPlayer(playerObj);
        voteManager.StartMeeting();
    }

    private void ResetMeetingPoints()
    {
        meetingPoints.Clear();

        foreach(Transform point in meetingPlace)
        {
            meetingPoints.Add(point);
        }
    }

    [PunRPC]
    public void SeatPlayerRPC()
    {
        //if (photonView.IsMine)
        //{
        ResetMeetingPoints();
        PlayerManager player = RoleManager.Instance.GetMyPlayerManager();
        SeatPlayer(player.controller.gameObject);
        //}
    }

    public void SeatPlayer(GameObject player)
    {
        if (meetingPoints.Count == 0)
        {
            Debug.LogError("No available meeting points left!");
            return;
        }

        int meetingPointIndex = PhotonNetwork.LocalPlayer.ActorNumber-1;

        player.transform.position = meetingPoints[meetingPointIndex].position;
        player.transform.rotation = meetingPoints[meetingPointIndex].rotation;
        //freeze player
        player.GetComponent<PlayerController>().freezePlayer = true;
        // add sitting animation
        player.GetComponentInChildren<Animator>().SetBool("isSitting", true);
    }
}
