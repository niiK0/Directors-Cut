using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class ReportButtonScript : BaseReporter
{
    [SerializeField] List<Transform> meetingPoints = new List<Transform>();
    private const string UsedMeetingPointProperty = "UsedMeetingPoint";

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

        int randomMeetingPointIndex = Random.Range(0, meetingPoints.Count);

        PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable { { UsedMeetingPointProperty, randomMeetingPointIndex } });

        player.transform.position = meetingPoints[randomMeetingPointIndex].position;
        player.transform.rotation = meetingPoints[randomMeetingPointIndex].rotation;
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey(UsedMeetingPointProperty))
        {
            // Adjust the available spawn points list based on the updated property.
            int usedSpawnIndex = (int)propertiesThatChanged[UsedMeetingPointProperty];
            meetingPoints.RemoveAt(usedSpawnIndex);
        }
    }
}
