using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class ReportButtonScript : BaseReporter
{
    [SerializeField] List<Transform> meetingPoints = new List<Transform>();

    protected override void ReporterFunction(GameObject playerObj)
    {
        ResetMeetingPoints();
        SeatPlayer(playerObj);
    }

    private void ResetMeetingPoints()
    {
        meetingPoints.Clear();

        foreach(Transform point in meetingPlace)
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

        int meetingPointIndex = PhotonNetwork.LocalPlayer.ActorNumber-1;

        player.transform.position = meetingPoints[meetingPointIndex].position;
        player.transform.rotation = meetingPoints[meetingPointIndex].rotation;
    }
}
