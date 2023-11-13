using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBodyScript : BaseReporter
{
    VoteManager voteManager;
    [SerializeField] List<Transform> meetingPoints = new List<Transform>();

    private void Start()
    {
        voteManager = VoteManager.Instance;
    }

    protected override void ReporterFunction(GameObject playerObj)
    {
        Destroy(gameObject, 3f);
        ResetMeetingPoints();
        SeatPlayer(playerObj);
        voteManager.StartMeeting();
    }

    private void ResetMeetingPoints()
    {
        meetingPoints.Clear();

        foreach (Transform point in meetingPlace)
        {
            meetingPoints.Add(point);
        }
    }

    public void SeatPlayer(GameObject player)
    {
        if (meetingPoints.Count == 0)
        {
            Debug.LogError("No available meeting points left!");
            return;
        }

        int meetingPointIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;

        player.transform.position = meetingPoints[meetingPointIndex].position;
        player.transform.rotation = meetingPoints[meetingPointIndex].rotation;
        //freeze player
        player.GetComponent<PlayerController>().freezePlayer = true;
        // add sitting animation
    }
}
