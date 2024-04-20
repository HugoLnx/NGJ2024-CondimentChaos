using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

namespace Jam
{
    public class FullTrack : MonoBehaviour
    {
        private void Start()
        {
            CompleteTrack();
        }

        [Button]
        private void CompleteTrack()
        {
            TrackWaypoint[] waypoints = GetComponentsInChildren<TrackWaypoint>();

            foreach (TrackWaypoint waypoint in waypoints)
            {
                waypoint.EnsureBackReferenceConnections();
            }
        }
    }
}
